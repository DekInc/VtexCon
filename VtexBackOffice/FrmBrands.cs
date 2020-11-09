using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using VtexCon;
using VtexCon.Models;

namespace VtexBackOffice {
    public partial class FrmBrands : MetroForm {
        public ApiVtex ApiVtexO { get; set; }
        public FrmBrands() {
            InitializeComponent();
        }

        private void FrmBrands_Load(object sender, EventArgs e) {
            ApiVtexO = new ApiVtex();
            List<Brand> ListBrands = ApiVtexO.GetBrands();
            GridBrands.DataSource = ListBrands;
            //GridBrands.Bind
        }
    }
}
