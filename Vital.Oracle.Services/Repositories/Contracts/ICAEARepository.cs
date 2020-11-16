using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vital.Oracle.Services.Model;

namespace Vital.Oracle.Services.Repositories.Contracts
{
    public interface IAFIPRepository
    {
        dynamic GetSucursales();

        dynamic GetCabeceraEmisor();

        dynamic GetCabeceraReceptor(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetTributos(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetIVA(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetDetalle(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetPIE(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetReporteAfipPendiente(string tipoReporte);

        dynamic InsertarReportelectroAfip(ReporteElectronicoAFIPModel RepoCaea);

        dynamic ActualizarRepoAfip(ReporteElectronicoAFIPModel Repo);

        int GetGenerarCAEA(string cdSucursal, DateTime fechaInicio);

        string GetCAEAAsignado(DateTime fecha);

        dynamic GetC1(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetC2(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetDetalleCAE(DateTime fDesde, DateTime fhasta, string suc);

        dynamic GetOtras(DateTime fDesde, DateTime fhasta, string suc);

    }
}
