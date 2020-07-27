namespace EveryPay.Desktop.WindowsFormApp
{
    partial class MainMenuPanel
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
            this.btnLog = new System.Windows.Forms.Button();
            this.btnImportProducts = new System.Windows.Forms.Button();
            this.backBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSystemVriables = new System.Windows.Forms.Button();
            this.btnPointsVariable = new System.Windows.Forms.Button();
            this.adminProductsbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLog
            // 
            this.btnLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.Location = new System.Drawing.Point(672, 638);
            this.btnLog.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(478, 98);
            this.btnLog.TabIndex = 0;
            this.btnLog.Text = "Consultar Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnImportProducts
            // 
            this.btnImportProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportProducts.Location = new System.Drawing.Point(672, 510);
            this.btnImportProducts.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnImportProducts.Name = "btnImportProducts";
            this.btnImportProducts.Size = new System.Drawing.Size(478, 100);
            this.btnImportProducts.TabIndex = 1;
            this.btnImportProducts.Text = "Importar Productos desde archivos";
            this.btnImportProducts.UseVisualStyleBackColor = true;
            this.btnImportProducts.Click += new System.EventHandler(this.btnImportProducts_Click);
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(40, 854);
            this.backBtn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(150, 44);
            this.backBtn.TabIndex = 6;
            this.backBtn.Text = "Logout";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(658, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(498, 79);
            this.label1.TabIndex = 7;
            this.label1.Text = "Menu Principal";
            // 
            // btnSystemVriables
            // 
            this.btnSystemVriables.Location = new System.Drawing.Point(672, 400);
            this.btnSystemVriables.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSystemVriables.Name = "btnSystemVriables";
            this.btnSystemVriables.Size = new System.Drawing.Size(478, 98);
            this.btnSystemVriables.TabIndex = 8;
            this.btnSystemVriables.Text = "Administrar lista negra de proveedores";
            this.btnSystemVriables.UseVisualStyleBackColor = true;
            this.btnSystemVriables.Click += new System.EventHandler(this.btnSystemVriables_Click);
            // 
            // btnPointsVariable
            // 
            this.btnPointsVariable.Location = new System.Drawing.Point(672, 290);
            this.btnPointsVariable.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPointsVariable.Name = "btnPointsVariable";
            this.btnPointsVariable.Size = new System.Drawing.Size(478, 98);
            this.btnPointsVariable.TabIndex = 9;
            this.btnPointsVariable.Text = "Administrar variable de puntos";
            this.btnPointsVariable.UseVisualStyleBackColor = true;
            this.btnPointsVariable.Click += new System.EventHandler(this.btnPointsVariable_Click);
            // 
            // adminProductsbtn
            // 
            this.adminProductsbtn.Location = new System.Drawing.Point(673, 205);
            this.adminProductsbtn.Name = "adminProductsbtn";
            this.adminProductsbtn.Size = new System.Drawing.Size(483, 76);
            this.adminProductsbtn.TabIndex = 10;
            this.adminProductsbtn.Text = "Administrar Productos";
            this.adminProductsbtn.UseVisualStyleBackColor = true;
            this.adminProductsbtn.Click += new System.EventHandler(this.adminProductsbtn_Click);
            // 
            // MainMenuPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.adminProductsbtn);
            this.Controls.Add(this.btnPointsVariable);
            this.Controls.Add(this.btnSystemVriables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.btnImportProducts);
            this.Controls.Add(this.btnLog);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainMenuPanel";
            this.Size = new System.Drawing.Size(1788, 925);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnImportProducts;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSystemVriables;
        private System.Windows.Forms.Button btnPointsVariable;
        private System.Windows.Forms.Button adminProductsbtn;
    }
}
