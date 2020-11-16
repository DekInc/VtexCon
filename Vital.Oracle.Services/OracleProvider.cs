using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.Oracle.Services.Encryption;

namespace Vital.Oracle.Services
{
    public class OracleProvider : IDbProvider
    {
        public const string ConnectionStringName = "OracleCN";

        private string ConnectionString { get; set; }

        public IDbConnection GetConnection(string connectionString = null)
        {
#if DEBUG
            if (connectionString == null && ConfigurationManager.ConnectionStrings[OracleProvider.ConnectionStringName] == null)
            {
                this.ConnectionString = "Data Source=develop;User Id=posapp;Password=posapp;";
            }
            else
            {
                this.ConnectionString = connectionString ?? ConfigurationManager.ConnectionStrings[OracleProvider.ConnectionStringName].ConnectionString;
            }
#else
            this.ConnectionString = StringCipher.Decrypt(connectionString ??
                                        ConfigurationManager.ConnectionStrings[OracleProvider.ConnectionStringName].ConnectionString, StringCipher.SALT);

#endif
            return new OracleConnection(this.ConnectionString);
        }

        public IDbCommand GetCommand(string packageName, IDbConnection dbConnection)
        {
            OracleCommand command = new OracleCommand(packageName, (OracleConnection)dbConnection);

            command.BindByName = true;

            return command;
        }

        public DbParameter GetParameter(string name, object value, ParameterDirection direction, bool IsCursor)
        {
            DbParameter dbParameter;

            if (IsCursor)
            {
                dbParameter = new OracleParameter(name, OracleDbType.RefCursor, direction);
            }
            else
            {
                dbParameter = new OracleParameter(name, GetOracleDbType(value), direction);
                dbParameter.Value = value;
                dbParameter.Size = 2000;
            }

            return dbParameter;
        }

        public IDbTransaction GetTransaction(IDbConnection dbConnection)
        {
            return dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        private OracleDbType GetOracleDbType(object o)
        {
            if (o is string) return OracleDbType.Varchar2;
            if (o is DateTime) return OracleDbType.Date;
            if (o is Int64) return OracleDbType.Int64;
            if (o is Int32) return OracleDbType.Int32;
            if (o is Int16) return OracleDbType.Int16;
            if (o is byte) return OracleDbType.Byte;
            if (o is decimal) return OracleDbType.Decimal;
            if (o is float) return OracleDbType.Single;
            if (o is double) return OracleDbType.Double;
            if (o is byte[]) return OracleDbType.Blob;

            return OracleDbType.Char;
            //throw new NotSupportedException(string.Format("El tipo {0} no es un formato válido para la conversión a tipo de dato Oracle.", o.GetType().FullName));
        }
    }
}
