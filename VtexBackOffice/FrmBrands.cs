using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using VtexCon;
using VtexCon.Models;

namespace VtexBackOffice {
    public partial class FrmBrands : MetroForm {
        public ApiVtex ApiVtexO { get; set; }
        public List<Brand> ListBrands { get; set; }
        public List<string> ListCols { get; set; }
        public FrmBrands() {
            InitializeComponent();
        }

        private void FrmBrands_Load(object sender, EventArgs e) {
            ApiVtexO = new ApiVtex();
            TmrLoad.Start();
        }

        private void BtnExcel_Click(object sender, EventArgs e) {
            StreamWriter ExcelOut = new StreamWriter("Brands.csv", false, Encoding.UTF8);
            ListCols = new List<string>();
            foreach (PropertyInfo PropCol in (typeof(Brand)).GetProperties()) {
                ListCols.Add(PropCol.Name);
                ExcelOut.Write(PropCol.Name + "Ç");
            }
            ExcelOut.WriteLine("");
            PbExport.Value = 0;
            PbExport.Maximum = ListBrands.Count;
            for (int i = 0; i < ListBrands.Count; i++) {
                PbExport.Value++;
                foreach (string PropCol in ListCols) {
                    ExcelOut.Write(ListBrands[i].GetType().GetProperty(PropCol).GetValue(ListBrands[i]) + "Ç");
                }
                ExcelOut.WriteLine("");
            }
            ExcelOut.Close();
        }

        private void TmrLoad_Tick(object sender, EventArgs e) {
            ListBrands = ApiVtexO.GetBrands();
            GridBrands.DataSource = ListBrands.GetRange(0, 10);
            TmrLoad.Stop();
        }
    }
}
