using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.Oracle.Services.Logging;
using Vital.Oracle.Services.Extensions;
using Oracle.ManagedDataAccess.Client;

namespace Vital.Oracle.Services
{
    public class DBAccess : IDisposable
    {
        public IDbConnection _connection;

        protected IDbProvider _provider;

        internal bool _handleException = true;

        public ConcurrentDictionary<string, object> Parameters { get; protected set; }

        public static event EventHandler<Exception> ExceptionThrowed;

        public const int ERROR_CODE = -99;

        public DBAccess()
            : this(null, null, true)
        {

        }

        public DBAccess(string connectionString, IDbProvider provider, bool handleException)
        {
            this._handleException = handleException;

            this.Parameters = new ConcurrentDictionary<string, object>();

            _provider = provider ?? new OracleProvider();

            _connection = _provider.GetConnection(connectionString);

            this.OpenConnection();
        }

        internal DBAccess(bool handleException)
            : this(null, null, handleException)
        {

        }

        public int ExecuteNonQuery(string packageName, params DbParameter[] parameters)
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                int result = 0;

                using (var pc = new PerformanceTracker(packageName + " - " + parameters.ToText()))
                {
                    result = cmd.ExecuteNonQuery();
                }

                PopulateParameters(cmd);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Write("<int>ExecuteNonQuery", "Exception", ex.Message + " " + ex.StackTrace);
                if (this is DBTransactionalAccess)
                {
                    ((DBTransactionalAccess)this).Rollback();
                }
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return ERROR_CODE;
            }
        }

        public int ExecuteNonQueryScript(string script)
        {
            try
            {
                var cmd = _provider.GetCommand(script, this._connection);

                int result = 0;

                result = cmd.ExecuteNonQuery();

                return result;
            }
            catch (Exception ex)
            {
                Logger.Write("<int>ExecuteNonQueryScript", "Exception", ex.Message + " " + ex.StackTrace);
                if (this is DBTransactionalAccess)
                {
                    ((DBTransactionalAccess)this).Rollback();
                }
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return ERROR_CODE;
            }
        }

        public object ExecuteScalar(string packageName, params DbParameter[] parameters)
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                object result = null;

                using (var pc = new PerformanceTracker(packageName + " - " + parameters.ToText()))
                {
                    result = cmd.ExecuteScalar();
                }

                PopulateParameters(cmd);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Write("<object>ExecuteScalar", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return default(object);
            }
        }


        public object InternalExecuteScalar(string packageName, params DbParameter[] parameters)
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                object result = null;

                result = cmd.ExecuteScalar();

                PopulateParameters(cmd);

                return result;
            }
            catch(Exception ex)
            {
                Logger.Write("<object>InternalExecuteScalar", "Exception", ex.Message + " " + ex.StackTrace);
                return default(object);
            }
        }

        public IEnumerable<T> ExecuteReaderToObject<T>(string packageName, params DbParameter[] parameters) where T : new()
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                var reader = cmd.ExecuteReader();

                PopulateParameters(cmd);

                IEnumerable<T> result = null;

                using (var pc = new PerformanceTracker(packageName + " - " + parameters.ToText()))
                {
                    result = DataMapper.MapReaderToObjectList<T>(reader);
                }

                return result;

            }
            catch (Exception ex)
            {
                Logger.Write("<IEnumerable<T>>ExecuteReaderToObject", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                }

                    ExceptionThrowed(this, ex);
                return default(IEnumerable<T>);
            }
        }

        public dynamic ExecuteScriptReaderToObject<T>(string script) where T : new()
        {
            try
            {
                var cmd = _provider.GetCommand(script, this._connection);

                var reader = cmd.ExecuteReader();

                PopulateParameters(cmd);

                IEnumerable<T> result = null;

                result = DataMapper.MapReaderToObjectList<T>(reader);

                return result;
            }
            catch (Exception ex)
            {
                Logger.Write("<dymanic>ExecuteScriptReaderToObject", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return null;
            }
        }

        public dynamic ExecuteReaderToDynamic(string packageName, params DbParameter[] parameters)
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                var reader = cmd.ExecuteReader();

                PopulateParameters(cmd);

                dynamic result;

                using (var pc = new PerformanceTracker(packageName + " - " + parameters.ToText()))
                {
                    result = DataMapper.MapReaderToDynamicList(reader);
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Write("<dymanic>ExecuteReaderToDynamic", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return null;
            }
        }

        public dynamic ExecuteReaderToDynamic(string packageName)
        {
            try
            {
                var cmd = BuildCommand(packageName);

                var reader = cmd.ExecuteReader();

                PopulateParameters(cmd);

                dynamic result = null;

                using (var pc = new PerformanceTracker(packageName))
                {
                    result = DataMapper.MapReaderToDynamicList(reader);
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Write("<dymanic>ExecuteReaderToDynamic", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return null;
            }
        }

        public IDataReader ExecuteReader(string packageName, params DbParameter[] parameters)
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);
                IDataReader reader = null;

                using (var pc = new PerformanceTracker(packageName + " - " + parameters.ToText()))
                {
                    reader = cmd.ExecuteReader();
                }

                PopulateParameters(cmd);

                return reader;
            }
            catch (Exception ex)
            {
                Logger.Write("<IDataReader>ExecuteReader", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return default(IDataReader);
            }
        }

        protected IDbCommand BuildCommand(string packageName)
        {
            return this.BuildCommand(packageName, null);
        }

        private IDbTransaction GetTransaction()
        {
            if (this is DBTransactionalAccess)
            {
                return ((DBTransactionalAccess)this)._transaction;
            }
            else
                return null;
        }

        protected IDbCommand BuildCommand(string packageName, DbParameter[] parameters)
        {
            var cmd = _provider.GetCommand(packageName, this._connection);

            cmd.Transaction = GetTransaction();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (parameters != null)
            {
                parameters.ToList().ForEach(p => cmd.Parameters.Add(p));
            }

            return cmd;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public DbParameter GetParameter(string name, object value, ParameterDirection direction, bool IsCursor)
        {
            return _provider.GetParameter(name, value, direction, IsCursor);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.CloseConnection();

                GC.SuppressFinalize(this);
            }
        }

        protected void PopulateParameters(IDbCommand cmd)
        {
            this.Parameters = new ConcurrentDictionary<string, object>();

            if (cmd != null && cmd.Parameters != null)
            {
                foreach (DbParameter parameter in cmd.Parameters)
                {
                    this.Parameters.AddOrUpdate(parameter.ParameterName, parameter.Value, (key, oldValue) => oldValue = parameter.Value);
                }
            }
        }

        protected void OpenConnection()
        {
            try
            {
                if (this._connection != null && _connection.State == ConnectionState.Open)
                {
                    throw new ApplicationException("La conexión ya estaba abierta.");
                }

                _connection.Open();

            }
            catch (OracleException ora)
            {
                Logger.Write("OpenConnection", "OracleException", ora.Message + " " + ora.StackTrace);
                if (_handleException)
                {
                    // Se registra el error al realizar la consulta, no es necesario registrar también error de conexión.
                    //ExceptionThrowed(this, Oex);
                }
                else
                {
                    throw;
                }
            }
            catch(Exception ex)
            {
                Logger.Write("OpenConnection", "Exception", ex.Message + " " + ex.StackTrace);
                if (_handleException)
                {
                    // Se registra el error al realizar la consulta, no es necesario registrar también error de conexión.
                    //ExceptionThrowed(this, ex);
                }
                else
                {
                    throw;
                }
            }
        }

        protected void CloseConnection()
        {
            try
            {
                if (this._connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch (Exception)
            {
                if (_handleException)
                {
                    // Se registra el error al realizar la consulta, no es necesario registrar también error de conexión.
                    //ExceptionThrowed(this, ex);
                }
            }
        }


        public IEnumerable<T> ExecuteReaderToObjectL<T>(string packageName, int rows, params DbParameter[] parameters) where T : new()
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                var reader = cmd.ExecuteReader();

                ((OracleDataReader)reader).FetchSize = ((OracleDataReader)reader).FetchSize * rows;

                PopulateParameters(cmd);

                IEnumerable<T> result = null;

                result = DataMapper.MapReaderToObjectList<T>(reader);

                return result;

            }
            catch (Exception ex)
            {
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return default(IEnumerable<T>);
            }
        }

        public dynamic ExecuteReaderToDynamicL(string packageName, int rows, params DbParameter[] parameters)
        {
            try
            {
                var cmd = BuildCommand(packageName, parameters);

                var reader = cmd.ExecuteReader();

                ((OracleDataReader)reader).FetchSize = ((OracleDataReader)reader).FetchSize * rows;

                PopulateParameters(cmd);

                dynamic result;

                result = DataMapper.MapReaderToDynamicList(reader);

                return result;
            }
            catch (Exception ex)
            {
                if (_handleException)
                {
                    ExceptionThrowed(this, ex);
                }

                return null;
            }
        }
    }
}
