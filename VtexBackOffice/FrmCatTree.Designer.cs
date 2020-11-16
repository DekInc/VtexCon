namespace VtexBackOffice {
    partial class FrmCatTree {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Nodo2");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Nodo0", new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Nodo3");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Nodo1", new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.PbExport = new MetroFramework.Controls.MetroProgressBar();
            this.BtnExcel = new MetroFramework.Controls.MetroButton();
            this.TmrLoad = new System.Windows.Forms.Timer(this.components);
            this.CatTree1 = new System.Windows.Forms.TreeView();
            this.GridCatTree = new System.Windows.Forms.DataGridView();
            this.BtnSave = new MetroFramework.Controls.MetroButton();
            this.BtnNew = new MetroFramework.Controls.MetroButton();
            this.BtnImportar = new MetroFramework.Controls.MetroButton();
            this.BtnExportar = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridCatTree)).BeginInit();
            this.SuspendLayout();
            // 
            // PbExport
            // 
            this.PbExport.Location = new System.Drawing.Point(104, 63);
            this.PbExport.Name = "PbExport";
            this.PbExport.Size = new System.Drawing.Size(115, 23);
            this.PbExport.Step = 1;
            this.PbExport.TabIndex = 8;
            // 
            // BtnExcel
            // 
            this.BtnExcel.Location = new System.Drawing.Point(23, 63);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(75, 23);
            this.BtnExcel.TabIndex = 7;
            this.BtnExcel.Text = "A Excel";
            this.BtnExcel.UseSelectable = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // TmrLoad
            // 
            this.TmrLoad.Interval = 1;
            this.TmrLoad.Tick += new System.EventHandler(this.TmrLoad_Tick);
            // 
            // CatTree1
            // 
            this.CatTree1.Location = new System.Drawing.Point(23, 92);
            this.CatTree1.Name = "CatTree1";
            treeNode5.Name = "Nodo2";
            treeNode5.Text = "Nodo2";
            treeNode6.Name = "Nodo0";
            treeNode6.Text = "Nodo0";
            treeNode7.Name = "Nodo3";
            treeNode7.Text = "Nodo3";
            treeNode8.Name = "Nodo1";
            treeNode8.Text = "Nodo1";
            this.CatTree1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode8});
            this.CatTree1.Size = new System.Drawing.Size(271, 213);
            this.CatTree1.TabIndex = 9;
            this.CatTree1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CatTree1_AfterSelect);
            // 
            // GridCatTree
            // 
            this.GridCatTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridCatTree.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridCatTree.Location = new System.Drawing.Point(23, 311);
            this.GridCatTree.MultiSelect = false;
            this.GridCatTree.Name = "GridCatTree";
            this.GridCatTree.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.GridCatTree.Size = new System.Drawing.Size(1031, 311);
            this.GridCatTree.TabIndex = 10;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(343, 63);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(112, 23);
            this.BtnSave.TabIndex = 11;
            this.BtnSave.Text = "Guardar";
            this.BtnSave.UseSelectable = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.Location = new System.Drawing.Point(225, 63);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(112, 23);
            this.BtnNew.TabIndex = 12;
            this.BtnNew.Text = "Nuevo";
            this.BtnNew.UseSelectable = true;
            // 
            // BtnImportar
            // 
            this.BtnImportar.Location = new System.Drawing.Point(579, 63);
            this.BtnImportar.Name = "BtnImportar";
            this.BtnImportar.Size = new System.Drawing.Size(112, 23);
            this.BtnImportar.TabIndex = 13;
            this.BtnImportar.Text = "Importar";
            this.BtnImportar.UseSelectable = true;
            this.BtnImportar.Click += new System.EventHandler(this.BtnImportar_Click);
            // 
            // BtnExportar
            // 
            this.BtnExportar.Location = new System.Drawing.Point(461, 63);
            this.BtnExportar.Name = "BtnExportar";
            this.BtnExportar.Size = new System.Drawing.Size(112, 23);
            this.BtnExportar.TabIndex = 14;
            this.BtnExportar.Text = "Exportar";
            this.BtnExportar.UseSelectable = true;
            this.BtnExportar.Click += new System.EventHandler(this.BtnExportar_Click);
            // 
            // FrmCatTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 645);
            this.Controls.Add(this.BtnExportar);
            this.Controls.Add(this.BtnImportar);
            this.Controls.Add(this.BtnNew);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.GridCatTree);
            this.Controls.Add(this.CatTree1);
            this.Controls.Add(this.PbExport);
            this.Controls.Add(this.BtnExcel);
            this.Name = "FrmCatTree";
            this.Text = "CatTree";
            this.Load += new System.EventHandler(this.FrmCatTree_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridCatTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroProgressBar PbExport;
        private MetroFramework.Controls.MetroButton BtnExcel;
        private System.Windows.Forms.Timer TmrLoad;
        private System.Windows.Forms.TreeView CatTree1;
        private System.Windows.Forms.DataGridView GridCatTree;
        private MetroFramework.Controls.MetroButton BtnSave;
        private MetroFramework.Controls.MetroButton BtnNew;
        private MetroFramework.Controls.MetroButton BtnImportar;
        private MetroFramework.Controls.MetroButton BtnExportar;
    }
}