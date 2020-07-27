namespace EveryPay.Desktop.WindowsFormApp
{
    partial class ProductAdministrationPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.addProductbtn = new System.Windows.Forms.Button();
            this.deleteProductbtn = new System.Windows.Forms.Button();
            this.panelProduct = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.saveProductbtn = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblAmountPoints = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.listProducts = new System.Windows.Forms.ListBox();
            this.errorlbl = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.panelProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Productos ";
            // 
            // addProductbtn
            // 
            this.addProductbtn.Location = new System.Drawing.Point(285, 138);
            this.addProductbtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addProductbtn.Name = "addProductbtn";
            this.addProductbtn.Size = new System.Drawing.Size(98, 28);
            this.addProductbtn.TabIndex = 2;
            this.addProductbtn.Text = "Agregar Producto";
            this.addProductbtn.UseVisualStyleBackColor = true;
            this.addProductbtn.Click += new System.EventHandler(this.addProductbtn_Click);
            // 
            // deleteProductbtn
            // 
            this.deleteProductbtn.Location = new System.Drawing.Point(285, 244);
            this.deleteProductbtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteProductbtn.Name = "deleteProductbtn";
            this.deleteProductbtn.Size = new System.Drawing.Size(98, 30);
            this.deleteProductbtn.TabIndex = 3;
            this.deleteProductbtn.Text = "Borrar Producto";
            this.deleteProductbtn.UseVisualStyleBackColor = true;
            this.deleteProductbtn.Click += new System.EventHandler(this.deleteProductbtn_Click);
            // 
            // panelProduct
            // 
            this.panelProduct.Controls.Add(this.btnUpdate);
            this.panelProduct.Controls.Add(this.saveProductbtn);
            this.panelProduct.Controls.Add(this.txtDescription);
            this.panelProduct.Controls.Add(this.txtStock);
            this.panelProduct.Controls.Add(this.txtPoints);
            this.panelProduct.Controls.Add(this.txtName);
            this.panelProduct.Controls.Add(this.lblStock);
            this.panelProduct.Controls.Add(this.lblAmountPoints);
            this.panelProduct.Controls.Add(this.lblDescription);
            this.panelProduct.Controls.Add(this.lblProductName);
            this.panelProduct.Location = new System.Drawing.Point(413, 66);
            this.panelProduct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelProduct.Name = "panelProduct";
            this.panelProduct.Size = new System.Drawing.Size(344, 300);
            this.panelProduct.TabIndex = 4;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(74, 233);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(84, 23);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Actualizar";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // saveProductbtn
            // 
            this.saveProductbtn.Location = new System.Drawing.Point(224, 233);
            this.saveProductbtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveProductbtn.Name = "saveProductbtn";
            this.saveProductbtn.Size = new System.Drawing.Size(82, 23);
            this.saveProductbtn.TabIndex = 9;
            this.saveProductbtn.Text = "Guardar";
            this.saveProductbtn.UseVisualStyleBackColor = true;
            this.saveProductbtn.Click += new System.EventHandler(this.saveProductbtn_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(147, 60);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(180, 66);
            this.txtDescription.TabIndex = 8;
            this.txtDescription.Text = "";
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(147, 187);
            this.txtStock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(120, 20);
            this.txtStock.TabIndex = 7;
            // 
            // txtPoints
            // 
            this.txtPoints.Location = new System.Drawing.Point(147, 147);
            this.txtPoints.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.Size = new System.Drawing.Size(114, 20);
            this.txtPoints.TabIndex = 6;
            this.txtPoints.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(146, 30);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 20);
            this.txtName.TabIndex = 4;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(37, 187);
            this.lblStock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(41, 13);
            this.lblStock.TabIndex = 3;
            this.lblStock.Text = "Stock :";
            // 
            // lblAmountPoints
            // 
            this.lblAmountPoints.AutoSize = true;
            this.lblAmountPoints.Location = new System.Drawing.Point(37, 150);
            this.lblAmountPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAmountPoints.Name = "lblAmountPoints";
            this.lblAmountPoints.Size = new System.Drawing.Size(102, 13);
            this.lblAmountPoints.TabIndex = 2;
            this.lblAmountPoints.Text = "Cantidad de puntos:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(36, 72);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(69, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Descripcion :";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(37, 33);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(50, 13);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Nombre :";
            // 
            // listProducts
            // 
            this.listProducts.FormattingEnabled = true;
            this.listProducts.Location = new System.Drawing.Point(30, 66);
            this.listProducts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listProducts.Name = "listProducts";
            this.listProducts.Size = new System.Drawing.Size(214, 290);
            this.listProducts.TabIndex = 5;
            this.listProducts.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // errorlbl
            // 
            this.errorlbl.AutoSize = true;
            this.errorlbl.Location = new System.Drawing.Point(28, 383);
            this.errorlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.errorlbl.Name = "errorlbl";
            this.errorlbl.Size = new System.Drawing.Size(0, 13);
            this.errorlbl.TabIndex = 6;
            // 
            // backBtn
            // 
            this.backBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backBtn.Location = new System.Drawing.Point(30, 434);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 8;
            this.backBtn.Text = "<-";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // ProductAdministrationPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.errorlbl);
            this.Controls.Add(this.listProducts);
            this.Controls.Add(this.panelProduct);
            this.Controls.Add(this.deleteProductbtn);
            this.Controls.Add(this.addProductbtn);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ProductAdministrationPanel";
            this.Size = new System.Drawing.Size(894, 481);
            this.Load += new System.EventHandler(this.ProductAdministrationPanel_Load);
            this.panelProduct.ResumeLayout(false);
            this.panelProduct.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addProductbtn;
        private System.Windows.Forms.Button deleteProductbtn;
        private System.Windows.Forms.Panel panelProduct;
        private System.Windows.Forms.Button saveProductbtn;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblAmountPoints;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ListBox listProducts;
        private System.Windows.Forms.Label errorlbl;
        private System.Windows.Forms.Button backBtn;
    }
}
