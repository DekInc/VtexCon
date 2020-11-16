namespace VtexBackOffice {
    partial class FrmMain {
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
            this.BtnBrands = new MetroFramework.Controls.MetroButton();
            this.BtnExit = new MetroFramework.Controls.MetroButton();
            this.BtnCatTree = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // BtnBrands
            // 
            this.BtnBrands.Location = new System.Drawing.Point(364, 63);
            this.BtnBrands.Name = "BtnBrands";
            this.BtnBrands.Size = new System.Drawing.Size(144, 23);
            this.BtnBrands.TabIndex = 0;
            this.BtnBrands.Text = "Brands";
            this.BtnBrands.UseSelectable = true;
            this.BtnBrands.Click += new System.EventHandler(this.BtnBrands_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(891, 489);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 23);
            this.BtnExit.TabIndex = 1;
            this.BtnExit.Text = "Salir";
            this.BtnExit.UseSelectable = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnCatTree
            // 
            this.BtnCatTree.Location = new System.Drawing.Point(364, 92);
            this.BtnCatTree.Name = "BtnCatTree";
            this.BtnCatTree.Size = new System.Drawing.Size(144, 23);
            this.BtnCatTree.TabIndex = 2;
            this.BtnCatTree.Text = "Category Tree";
            this.BtnCatTree.UseSelectable = true;
            this.BtnCatTree.Click += new System.EventHandler(this.BtnCatTree_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 535);
            this.Controls.Add(this.BtnCatTree);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnBrands);
            this.Name = "FrmMain";
            this.Text = "Vtex BackOffice";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton BtnBrands;
        private MetroFramework.Controls.MetroButton BtnExit;
        private MetroFramework.Controls.MetroButton BtnCatTree;
    }
}

