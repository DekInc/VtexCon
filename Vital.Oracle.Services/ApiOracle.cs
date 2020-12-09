using log4net;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.Oracle.Services.Helpers;
using Vital.Oracle.Services.Model;
using VtexCon.ModelsOra.Model;
using Vital.Oracle.Services.Extensions;

namespace Vital.Oracle.Services {
    public class ApiOracle {
        public IEnumerable<VtexProduct> GetProducts() {
            Common.LogInfo("Vtex ApiOracle GetProducts Crear conexión");
            using (DBAccess db = new DBAccess()) {
                Common.LogInfo("Vtex ApiOracle GetProducts conexión creada");
                return db.ExecuteReaderToObject<VtexProduct>($"{PackagesName.VitalVtex}{ProceduresName.GetProduct}",
                    new OracleParameter("Cur_Out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
            }
        }
        public SpRet SetProduct(SetProduct RefId) {
            SpRet Res = new SpRet();
            try {
                Common.LogInfo("Vtex ApiOracle SetProduct Crear conexión");
                using (DBAccess db = new DBAccess()) {
                    Common.LogInfo("Vtex ApiOracle SetProduct conexión creada");
                    string RefIdConcat = RefId.RefId.Get() + RefId.Status.Get() + RefId.ProductId.Get() + RefId.Detail.Get();
                    OracleParameterCollection OracleParameters = (new OracleCommand()).Parameters;
                    OracleParameters.AddArray<string>(
                        "p_refId",
                        OracleDbType.Varchar2,
                        new string[] { RefIdConcat },
                        ParameterDirection.Input,
                        null);
                    OracleParameters.Add(new OracleParameter("p_Ok", OracleDbType.Int32, 1, null, ParameterDirection.Output));
                    OracleParameters.Add(new OracleParameter("p_error", OracleDbType.Varchar2, 6666, null, ParameterDirection.Output));
                    dynamic Ret = db.ExecuteReaderToDynamicNative($"{PackagesName.VitalVtex}{ProceduresName.SetProduct}", ref OracleParameters);
                    Res = new SpRet() {
                        Ok = Convert.ToDecimal(OracleParameters["p_Ok"].Value.ToString()),
                        Error = OracleParameters["p_Ok"].Value.ToString()
                    };
                }
            } catch (Exception E1) {
                Common.LogError(E1.ToString() + E1.StackTrace);
            }
            return Res;
        }
        public SpRet SetSku(SetProduct RefId) {
            SpRet Res = new SpRet();
            try {
                Common.LogInfo("Vtex ApiOracle SetSku Crear conexión");
                using (DBAccess db = new DBAccess()) {
                    Common.LogInfo("Vtex ApiOracle SetSku conexión creada");
                    string RefIdConcat = RefId.RefId.Get() + RefId.Status.Get() + RefId.ProductId.Get() + RefId.Detail.Get();
                    OracleParameterCollection OracleParameters = (new OracleCommand()).Parameters;
                    OracleParameters.AddArray<string>(
                        "p_refId",
                        OracleDbType.Varchar2,
                        new string[] { RefIdConcat },
                        ParameterDirection.Input,
                        null);
                    OracleParameters.Add(new OracleParameter("p_Ok", OracleDbType.Int32, 1, null, ParameterDirection.Output));
                    OracleParameters.Add(new OracleParameter("p_error", OracleDbType.Varchar2, 6666, null, ParameterDirection.Output));
                    dynamic Ret = db.ExecuteReaderToDynamicNative($"{PackagesName.VitalVtex}{ProceduresName.SetSku}", ref OracleParameters);
                    Res = new SpRet() {
                        Ok = Convert.ToDecimal(OracleParameters["p_Ok"].Value.ToString()),
                        Error = OracleParameters["p_error"].Value.ToString()
                    };
                }
            } catch (Exception E1) {
                Common.LogError(E1.ToString() + E1.StackTrace);
            }
            return Res;
        }
        public void WriteOraLog(int Code, string Msj) {
            using (DBAccess db = new DBAccess()) {
                db.ExecuteNonQuery($"{PackagesName.LogGeneral}{ProceduresName.Wr1te}",
                    new OracleParameter("pi_codigo", OracleDbType.Int32, 2000, Code, ParameterDirection.Input),
                    new OracleParameter("pi_mensaje", OracleDbType.Varchar2, 4000, Msj, ParameterDirection.Input));
            }
        }
        public IEnumerable<VtexNewSku> GetSkus() {
            Common.LogInfo("Vtex ApiOracle GetSkus Crear conexión");
            using (DBAccess db = new DBAccess()) {
                Common.LogInfo("Vtex ApiOracle GetSkus conexión creada");
                return db.ExecuteReaderToObject<VtexNewSku>($"{PackagesName.VitalVtex}{ProceduresName.GetSku}",
                    new OracleParameter("Cur_Out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
            }
        }
        public List<GetTokens> GetTokens(bool IsMain, bool IsOthers) {
            Common.LogInfo("Vtex ApiOracle GetTokens Crear conexión");
            using (DBAccess db = new DBAccess()) {
                Common.LogInfo("Vtex ApiOracle GetTokens conexión creada");
                return db.ExecuteReaderToObject<GetTokens>($"{PackagesName.VitalVtex}{ProceduresName.GetTokens}",
                    new OracleParameter("p_main", OracleDbType.Int32, 1, IsMain? 1 : 0, ParameterDirection.Input),
                    new OracleParameter("p_idcanal", OracleDbType.Int32, 1, IsOthers? 1 : 0, ParameterDirection.Input),
                    new OracleParameter("Cur_Out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
            }
        }
        public IEnumerable<VtexStock> GetStock(string BranchId) {
            Common.LogInfo("Vtex ApiOracle GetStock Crear conexión");
            using (DBAccess db = new DBAccess()) {
                Common.LogInfo("Vtex ApiOracle GetStock conexión creada");
                return db.ExecuteReaderToObject<VtexStock>($"{PackagesName.VitalVtex}{ProceduresName.GetStock}",
                    new OracleParameter("p_cdSucursal", OracleDbType.Varchar2, 8, BranchId, ParameterDirection.Input),
                    new OracleParameter("Cur_Out", OracleDbType.RefCursor, 4000, null, ParameterDirection.Output));
            }
        }
    }
}
