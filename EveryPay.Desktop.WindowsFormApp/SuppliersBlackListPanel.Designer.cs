namespace EveryPay.Desktop.WindowsFormApp
{
    partial class SuppliersBlackListPanel
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
            this.backBtn = new System.Windows.Forms.Button();
            this.listBoxSuppliers = new System.Windows.Forms.ListBox();
            this.addOrRemoveBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(26, 439);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 8;
            this.backBtn.Text = "<-";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // listBoxSuppliers
            // 
            this.listBoxSuppliers.FormattingEnabled = true;
            this.listBoxSuppliers.Location = new System.Drawing.Point(180, 67);
            this.listBoxSuppliers.Name = "listBoxSuppliers";
            this.listBoxSuppliers.Size = new System.Drawing.Size(266, 368);
            this.listBoxSuppliers.TabIndex = 9;
            this.listBoxSuppliers.SelectedIndexChanged += new System.EventHandler(this.listBoxSuppliers_SelectedIndexChanged);
            // 
            // addOrRemoveBtn
            // 
            this.addOrRemoveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addOrRemoveBtn.Location = new System.Drawing.Point(474, 67);
            this.addOrRemoveBtn.Name = "addOrRemoveBtn";
            this.addOrRemoveBtn.Size = new System.Drawing.Size(202, 47);
            this.addOrRemoveBtn.TabIndex = 10;
            this.addOrRemoveBtn.Text = "Agregar a lista negra";
            this.addOrRemoveBtn.UseVisualStyleBackColor = true;
            this.addOrRemoveBtn.Click += new System.EventHandler(this.addOrRemoveBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(556, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "Administracion de lista negra de proveedores";
            // 
            // SuppliersBlackListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrRemoveBtn);
            this.Controls.Add(this.listBoxSuppliers);
            this.Controls.Add(this.backBtn);
            this.Name = "SuppliersBlackListPanel";
            this.Size = new System.Drawing.Size(894, 481);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.ListBox listBoxSuppliers;
        private System.Windows.Forms.Button addOrRemoveBtn;
        private System.Windows.Forms.Label label1;
    }
}
