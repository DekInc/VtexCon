using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VtexBackOffice {
    static public class Commons {
        static public void MsjInfo(string Msj) {
            MessageBox.Show(Msj, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
