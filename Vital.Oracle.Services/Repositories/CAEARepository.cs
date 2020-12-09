using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.Oracle.Services.Helpers;
using Vital.Oracle.Services.Model;
using Vital.Oracle.Services.Repositories.Contracts;

namespace Vital.Oracle.Services.Repositories
{
    public class AFIPRepository : IAFIPRepository
    {
        //public dynamic GetSucursales()
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.VitalPosCore + ProceduresName.ObtenerSucursales,
        //            new OracleParameter("cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetCabeceraEmisor()
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetCabeceraEmisor,
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}


        //public dynamic GetCabeceraReceptor(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetCabeceraReceptor,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetTributos(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetTributos,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetIVA(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetIVA,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetDetalle(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetDetalles,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetPIE(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetPIE,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetReporteAfipPendiente(string tipoReporte)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetReporteAfipPendiente,
        //            new OracleParameter("p_tipo", OracleDbType.Char, 4, tipoReporte, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic InsertarReportelectroAfip(ReporteElectronicoAFIPModel Repo)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.InsertarReportelectroAfip,
        //            new OracleParameter("p_cdsucursal", OracleDbType.Char, 8, Repo._CDSUCURSAL, ParameterDirection.Input),
        //            new OracleParameter("p_dtreporteafip", OracleDbType.Date, 30, Repo._DTREPORTEAFIP, ParameterDirection.Input),
        //            new OracleParameter("p_dtgenerado", OracleDbType.Date, 30, Repo._DTGENERADO, ParameterDirection.Input),
        //            new OracleParameter("p_vlestado", OracleDbType.Char, 1, Repo._VLESTADO, ParameterDirection.Input),
        //            new OracleParameter("p_dsestado", OracleDbType.Varchar2, 100, Repo._DSESTADO, ParameterDirection.Input),
        //            new OracleParameter("p_tipo", OracleDbType.Char, 4, Repo._TIPO, ParameterDirection.Input),
        //            new OracleParameter("p_ok", OracleDbType.Varchar2, 1, null, ParameterDirection.Output),
        //            new OracleParameter("p_error", OracleDbType.Varchar2, 100, null, ParameterDirection.Output));

        //        result = db.Parameters;
        //    }

        //    return result["p_error"].ToString().Trim() == "null" ? string.Empty : result["p_error"].ToString().Trim();
        //}

        //public int GetGenerarCAEA(string cdSucursal, DateTime fechaInicio)
        //{
        //    dynamic genero;

        //    using (var db = new DBAccess())
        //    {
        //        db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetGenerarArchivo,
        //            new OracleParameter("p_cdsucursal", OracleDbType.Char, 8, cdSucursal, ParameterDirection.Input),
        //            new OracleParameter("p_dtfecha", OracleDbType.Date, 30, fechaInicio, ParameterDirection.Input),
        //            new OracleParameter("p_generar", OracleDbType.Int32, 1, null, ParameterDirection.Output));

        //        genero = db.Parameters;
        //    }

        //    return int.Parse(genero["p_generar"].ToString());
        //}

        //public dynamic ActualizarRepoAfip(ReporteElectronicoAFIPModel Repo)
        //{
        //    dynamic result;
        //    using (var db = new DBAccess())
        //    {
        //        db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.ActualizarReportelectroAfip,
        //            new OracleParameter("p_cdsucursal", OracleDbType.Char, 8, Repo._CDSUCURSAL, ParameterDirection.Input),
        //            new OracleParameter("p_dtreporteafip", OracleDbType.Date, 30, Repo._DTREPORTEAFIP, ParameterDirection.Input),
        //            new OracleParameter("p_vlestado", OracleDbType.Char, 1, Repo._VLESTADO, ParameterDirection.Input),
        //            new OracleParameter("p_dsestado", OracleDbType.Varchar2, 100, Repo._DSESTADO, ParameterDirection.Input),
        //            new OracleParameter("p_tipo", OracleDbType.Char, 4, Repo._TIPO, ParameterDirection.Input),
        //            new OracleParameter("p_ok", OracleDbType.Varchar2, 1, null, ParameterDirection.Output),
        //            new OracleParameter("p_error", OracleDbType.Varchar2, 100, null, ParameterDirection.Output));

        //        result = db.Parameters;
        //    }
        //    return result["p_error"].ToString().Trim() == "null" ? string.Empty : result["p_error"].ToString().Trim();
        //}

        //public string GetCAEAAsignado(DateTime fecha)
        //{
        //    dynamic caea;

        //    using (var db = new DBAccess())
        //    {
        //        db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetCAEAAsignado,
        //            new OracleParameter("p_dtfecha", OracleDbType.Date, 30, fecha, ParameterDirection.Input),
        //            new OracleParameter("p_caea", OracleDbType.Varchar2, 30, null, ParameterDirection.Output));

        //        caea = db.Parameters;
        //    }

        //    return caea["p_caea"].ToString();
        //}

        //public dynamic GetC1(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetC1_CAE,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetC2(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetC2_CAE,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetDetalleCAE(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetDetalle_CAE,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}

        //public dynamic GetOtras(DateTime fDesde, DateTime fhasta, string suc)
        //{
        //    dynamic result;

        //    using (var db = new DBAccess())
        //    {
        //        result = db.ExecuteReaderToDynamic(PackagesName.FacturaElectronica + ProceduresName.GetOtras_CAE,
        //            new OracleParameter("p_dtfechadesde", OracleDbType.Date, 10, fDesde, ParameterDirection.Input),
        //            new OracleParameter("p_dtfechahasta", OracleDbType.Date, 10, fhasta, ParameterDirection.Input),
        //            new OracleParameter("p_cdsucursal", OracleDbType.Varchar2, 4, suc, ParameterDirection.Input),
        //            new OracleParameter("p_cur_out", OracleDbType.RefCursor, 2000, null, ParameterDirection.Output));
        //    }
        //    return result;
        //}
    }
}
