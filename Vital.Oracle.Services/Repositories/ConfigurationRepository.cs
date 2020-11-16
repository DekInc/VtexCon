using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Vital.Oracle.Services.Repositories.Contracts;

namespace Vital.Oracle.Services.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        public dynamic GetParameter(string [] parameters)
        {
            dynamic result;
            using (var db = new DBAccess())
            {
                result = db.InternalExecuteScalar("N_PKG_VITALPOS_CORE.GetParameter",
                    new OracleParameter("cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output),
                    new OracleParameter("pparam", OracleDbType.Varchar2, 2000, parameters[1], ParameterDirection.Input),
                    new OracleParameter("pdescriptor", OracleDbType.Varchar2, 2000, parameters[0], ParameterDirection.Input));
            }
            return result;
        }
    }
}
