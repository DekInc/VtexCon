using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Extensions
{
    public static class DBParamenterExtension
    {
        public static string ToText(this DbParameter[] dbParameters)
        {
            var result = new StringBuilder();

            if (dbParameters != null && dbParameters.Length > 0)
            {
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    result.AppendLine(string.Format("Parametro: {0}. Valor: {1}", dbParameters[i].ParameterName, dbParameters[i].Value == null ? "null" : dbParameters[i].Value.ToString()));
                }
            }

            return result.ToString();
        }
    }
}
