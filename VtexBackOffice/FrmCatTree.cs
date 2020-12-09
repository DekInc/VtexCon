using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Newtonsoft.Json;
using VtexCon;
using VtexCon.Models;
using Vital.Oracle;
using VtexCon.ModelsOra.Model;

namespace VtexBackOffice {
    public partial class FrmCatTree : MetroForm {
        public ApiVtex ApiVtexO { get; set; }
        public List<CategoryTree> ListCatTree { get; set; }
        public List<Category> ListCatTreeNormal { get; set; }
        public List<VtexCatalog> ListCatOracle { get; set; }
        public List<string> ListCols { get; set; }
        public FrmCatTree() {
            InitializeComponent();
            ApiVtexO = new ApiVtex(0, "", "", "");
        }

        private void TmrLoad_Tick(object sender, EventArgs e) {
            ListCatTree = ApiVtexO.GetCatTree(16);            
            ListCatTreeNormal = new List<Category>();
            CatTree1.Nodes.Clear();
            TreeNodeCollection MainNode = CatTree1.Nodes;
            PbExport.Value = 0;
            PbExport.Maximum = ListCatTree.Count;
            ConstructNormalTree(ref MainNode, ListCatTree);            
            TmrLoad.Stop();
        }

        private void ConstructNormalTree(ref TreeNodeCollection MainNode, List<CategoryTree> ListCatTreeSub) {
            foreach (CategoryTree Sub in ListCatTreeSub) {
                MainNode.Add(Sub.Id.ToString(), $"{Sub.Id} - {Sub.Name}");
                PbExport.Value++;
                //Application.DoEvents();
                foreach (CategoryTree SubSub in Sub.Children) {
                    SubSub.Father = Sub.Id;                    
                }
                TreeNodeCollection MainNodeSub = MainNode[MainNode.Count - 1].Nodes;
                ConstructNormalTree(ref MainNodeSub, Sub.Children);
            }
        }

        private void ConstructNormalTree4Export(TreeNodeCollection MainNode, List<CategoryTree> ListCatTreeSub) {
            foreach (CategoryTree Sub in ListCatTreeSub) {
                Category CategoryO = ApiVtexO.GetCategoryInfo(Sub.Id.ToString());
                ListCatTreeNormal.Add(CategoryO);
                PbExport.Value++;
                foreach (CategoryTree SubSub in Sub.Children) {
                    SubSub.Father = Sub.Id;
                }
                TreeNodeCollection MainNodeSub = MainNode[MainNode.Count - 1].Nodes;
                ConstructNormalTree4Export(MainNodeSub, Sub.Children);
            }
        }
        private void ConstructOracleTree4Export() {
            ListCatOracle = new List<VtexCatalog>();
            foreach (Category AllCat in ListCatTreeNormal) {
                if (ListCatTreeNormal.Where(O => Convert.ToInt32(O.FatherCategoryId) == AllCat.Id).Count() == 0) {
                    Category CatFather = ListCatTreeNormal.Where(O => O.Id == AllCat.FatherCategoryId).FirstOrDefault();
                    Category CatFatherFather = ListCatTreeNormal.Where(O => O.Id == CatFather.FatherCategoryId).FirstOrDefault();
                    ListCatOracle.Add(new VtexCatalog() {
                        SubCategoryId = AllCat.Id.Value,
                        SubCategoryName = AllCat.Name,
                        CategoryId = CatFather.Id.Value,
                        CategoryName = CatFather.Name,
                        DepartmentId = CatFatherFather.Id.Value,
                        DepartmentName = CatFatherFather.Name
                    });
                }
            }
        }        
        private void FrmCatTree_Load(object sender, EventArgs e) {
            TmrLoad.Start();
        }

        private void BtnExcel_Click(object sender, EventArgs e) {
            TreeNodeCollection MainNode = CatTree1.Nodes;
            PbExport.Value = 0;
            PbExport.Maximum = ListCatTree.Count;
            Status1.Text = "Creating Normal Tree";
            ConstructNormalTree4Export(MainNode, ListCatTree);
            Status1.Text = "Exporting Normal Tree";
            StreamWriter ExcelOut = new StreamWriter($"CatTree{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.csv", false, Encoding.UTF8);
            ListCols = new List<string>();
            foreach (PropertyInfo PropCol in (typeof(Category)).GetProperties()) {
                ListCols.Add(PropCol.Name);
                ExcelOut.Write(PropCol.Name + "Ç");
            }
            ExcelOut.WriteLine("");
            PbExport.Value = 0;
            PbExport.Maximum = ListCatTreeNormal.Count;
            for (int i = 0; i < ListCatTreeNormal.Count; i++) {
                PbExport.Value++;
                foreach (string PropCol in ListCols) {
                    ExcelOut.Write(ListCatTreeNormal[i].GetType().GetProperty(PropCol).GetValue(ListCatTreeNormal[i]) + "Ç");
                }
                ExcelOut.WriteLine("");
            }
            ExcelOut.Close();
            Status1.Text = "Exporting Oracle tree";
            ConstructOracleTree4Export();
            Status1.Text = "Exporting Normal Tree";
            StreamWriter ExcelOut2 = new StreamWriter($"CatTreeOracle{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.csv", false, Encoding.UTF8);
            ListCols = new List<string>();
            foreach (PropertyInfo PropCol in (typeof(VtexCatalog)).GetProperties()) {
                ListCols.Add(PropCol.Name);
                ExcelOut2.Write(PropCol.Name + "Ç");
            }
            ExcelOut2.WriteLine("");
            PbExport.Value = 0;
            PbExport.Maximum = ListCatOracle.Count;
            for (int i = 0; i < ListCatOracle.Count; i++) {
                PbExport.Value++;
                foreach (string PropCol in ListCols) {
                    ExcelOut2.Write(ListCatOracle[i].GetType().GetProperty(PropCol).GetValue(ListCatOracle[i]) + "Ç");
                }
                ExcelOut2.WriteLine("");
            }
            ExcelOut2.Close();
            Commons.MsjInfo("End");
        }

        private void CatTree1_AfterSelect(object sender, TreeViewEventArgs e) {
            Category CategoryO = ApiVtexO.GetCategoryInfo(e.Node.Name);
            List<Category> ListTemp = new List<Category>();
            ListTemp.Add(CategoryO);
            GridCatTree.DataSource = ListTemp;
            if (ListTemp.Count > 0)
                GridCatTree.Columns[0].ReadOnly = true;
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            Category ToUpdateCategory = (GridCatTree.DataSource as List<Category>).FirstOrDefault();
            Category UpdatedCategory = ApiVtexO.UpdateCategory(ToUpdateCategory);
        }

        private void BtnImportar_Click(object sender, EventArgs e) {
            StreamReader ExcelIn = new StreamReader("CatTree.csv", Encoding.UTF8);
            
            ExcelIn.Close();
        }

        private void BtnExportar_Click(object sender, EventArgs e) {
            TreeNodeCollection MainNode = CatTree1.Nodes;
            PbExport.Value = 0;
            PbExport.Maximum = ListCatTree.Count;
            ConstructNormalTree4Export(MainNode, ListCatTree);
            StreamWriter JsonOut = new StreamWriter($"Categories{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}.json", false, Encoding.UTF8);
            JsonOut.Write(JsonConvert.SerializeObject(ListCatTreeNormal));
            JsonOut.Close();
        }
    }
}
