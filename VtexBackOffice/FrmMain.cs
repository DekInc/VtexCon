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

namespace VtexBackOffice {
    public partial class FrmMain : MetroForm {
        public FrmMain() {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void BtnBrands_Click(object sender, EventArgs e) {
            FrmBrands FrmBrandsO = new FrmBrands();
            FrmBrandsO.ShowDialog();
        }
    }
}
