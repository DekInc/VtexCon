using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vital.Oracle.Services.Model
{
    public class ReporteElectronicoAFIPModel
    {
        public string _CDSUCURSAL { get; set; }
        public DateTime _DTREPORTEAFIP { get; set; }
        public DateTime? _DTGENERADO { get; set; }
        public string _VLESTADO { get; set; }
        public string _DSESTADO { get; set; }
        public string _TIPO { get; set; }

        public ReporteElectronicoAFIPModel(string cdSucursal, DateTime dtReporteAfip, DateTime? dtGenerado, string vlEstado, string dsEstado, string tipo)
        {
            _CDSUCURSAL = cdSucursal;
            _DTREPORTEAFIP = dtReporteAfip;
            _DTGENERADO = dtGenerado;
            _VLESTADO = vlEstado;
            _DSESTADO = dsEstado;
            _TIPO = tipo;
        }
    }
}
