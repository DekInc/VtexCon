using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static void AddArray<T>(
            this OracleParameterCollection parameters,
            string name,
            OracleDbType dbType,
            T[] array,
            ParameterDirection dir,
            T emptyArrayValue) {
            parameters.Add(new OracleParameter {
                ParameterName = name,
                OracleDbType = dbType,
                CollectionType = OracleCollectionType.PLSQLAssociativeArray
            });
            // oracle does not support passing null or empty arrays.
            // so pass an array with exactly one element
            // with a predefined value and use it to check
            // for empty array condition inside the proc code
            if (array == null || array.Length == 0) {
                parameters[name].Value = new T[1] { emptyArrayValue };
                parameters[name].Size = 1;
            } else {
                parameters[name].Value = array;
                parameters[name].Size = array.Length;
            }
        }
    }
}
