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
    public class LogGeneralRepository : ILogGeneralRepository
    {
        public void Write(string mensaje)
        {
            using (var db = new DBAccess())
            {
                db.ExecuteNonQuery("N_PKG_VITALPOS_LOG_GENERAL.Write",
                    new OracleParameter("pi_codigo", OracleDbType.Varchar2, 20, "1", ParameterDirection.Input),
                    new OracleParameter("pi_mensaje", OracleDbType.Varchar2, 2000, mensaje, ParameterDirection.Input));

            }
        }
    }
}
