namespace EveryPay.Desktop.WindowsFormApp
{
    partial class ImportProductsPanel
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textDllsRoute = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listDlls = new System.Windows.Forms.ListBox();
            this.btnSelectDll = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textDllsRoute
            // 
            this.textDllsRoute.Location = new System.Drawing.Point(248, 83);
            this.textDllsRoute.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textDllsRoute.Name = "textDllsRoute";
            this.textDllsRoute.ReadOnly = true;
            this.textDllsRoute.Size = new System.Drawing.Size(1080, 31);
            this.textDllsRoute.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1368, 83);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(226, 38);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Buscar DLLs";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ruta de las Dlls";
            // 
            // listDlls
            // 
            this.listDlls.FormattingEnabled = true;
            this.listDlls.ItemHeight = 25;
            this.listDlls.Location = new System.Drawing.Point(30, 190);
            this.listDlls.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listDlls.Name = "listDlls";
            this.listDlls.Size = new System.Drawing.Size(1298, 629);
            this.listDlls.TabIndex = 3;
            // 
            // btnSelectDll
            // 
            this.btnSelectDll.Location = new System.Drawing.Point(1344, 190);
            this.btnSelectDll.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSelectDll.Name = "btnSelectDll";
            this.btnSelectDll.Size = new System.Drawing.Size(412, 115);
            this.btnSelectDll.TabIndex = 4;
            this.btnSelectDll.Text = "Seleccionar DLL";
            this.btnSelectDll.UseVisualStyleBackColor = true;
            this.btnSelectDll.Click += new System.EventHandler(this.btnSelectDll_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(30, 858);
            this.backBtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(150, 44);
            this.backBtn.TabIndex = 5;
            this.backBtn.Text = "<-";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // ImportProductsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.btnSelectDll);
            this.Controls.Add(this.listDlls);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textDllsRoute);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ImportProductsPanel";
            this.Size = new System.Drawing.Size(1788, 925);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textDllsRoute;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listDlls;
        private System.Windows.Forms.Button btnSelectDll;
        private System.Windows.Forms.Button backBtn;
    }
}
