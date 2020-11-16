using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services
{
    public class DBTransactionalAccess : DBAccess
    {
        public IDbTransaction _transaction;

        public static event EventHandler<Exception> ExceptionThrowedHelp;

        public DBTransactionalAccess(string connectionString = null, IDbProvider provider = null, bool handleException = true)
        {
            this._handleException = handleException;

            this.Parameters = new ConcurrentDictionary<string, object>();

            _provider = provider ?? new OracleProvider();

            _connection = _provider.GetConnection(connectionString);

            this.OpenConnection();

            this._transaction = _provider.GetTransaction(_connection);
        }

        public void Rollback()
        {
            try
            {
                this._transaction.Rollback();
            }
            catch (Exception ex)
            {
                if (_handleException)
                {
                    ExceptionThrowedHelp(this, ex);
                }
            }
        }

        public void Commit()
        {
            try
            {
                this._transaction.Commit();
            }
            catch (Exception ex)
            {
                if (_handleException)
                {
                    ExceptionThrowedHelp(this, ex);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._transaction.Dispose();

                GC.SuppressFinalize(this);
            }

            base.Dispose(disposing);
        }
    }
}
