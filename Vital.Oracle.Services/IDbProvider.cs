using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services
{
    public interface IDbProvider
    {
        IDbCommand GetCommand(string packageName, IDbConnection dbConnection);

        IDbConnection GetConnection(string connectionString = null);

        IDbTransaction GetTransaction(IDbConnection dbConnection);

        DbParameter GetParameter(string name, object value, ParameterDirection direction, bool IsCursor);
    }
}
