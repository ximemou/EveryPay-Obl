using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveryPay.Desktop.LogicController;
using EveryPay.Data.Entities;

namespace EveryPay.Desktop.WindowsFormApp
{
    public partial class ProductAdministrationPanel : UserControl
    {

        public MainForm MainForm { get; set; }
        private DesktopLogicController DesktopLogicController { get; set; }

        public ProductAdministrationPanel(MainForm main)
        {

            InitializeComponent();
            MainForm = main;
            DesktopLogicController = new DesktopLogicController();
            loadProducts();
            panelProduct.Visible = false;
            
        }

        private void loadProducts()
        {
            List<Product> products = DesktopLogicController.getAllProducts();
            if (products.Count > 0)
            {

                listProducts.DataSource = products;
            }
            else
            {
                errorlbl.Text = "No existen productos en el sistema";
            }
        }

        private void addProductbtn_Click(object sender, EventArgs e)
        {

            panelProduct.Visible = true;
            clearInputs();
            btnUpdate.Visible = false;
            saveProductbtn.Visible = true;
        }


        private bool validateStringFields(string name, string description)
        {

            if (name.Length > 0 && description.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void ProductAdministrationPanel_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listProducts.SelectedIndex == -1)
            {
               return;
            }
               

           Product selectedProduct = (Product)listProducts.SelectedItem;


            panelProduct.Visible = true;
            btnUpdate.Visible = true;
            saveProductbtn.Visible = false;

            txtName.Text = selectedProduct.Name;
            txtDescription.Text = selectedProduct.Description;
            txtPoints.Text = "" + selectedProduct.RequiredPoints;
            txtStock.Text = "" + selectedProduct.NumberInStock;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void saveProductbtn_Click(object sender, EventArgs e)
        {

            validateInputs();
            string name = txtName.Text;
            string description = txtDescription.Text;
            int points = int.Parse(txtPoints.Text);
            int stock = int.Parse(txtStock.Text);

            Product product = new Product();
            product.Name = name;
            product.Description = description;
            product.NumberInStock = stock;
            product.RequiredPoints = points;

            DesktopLogicController.saveProduct(product);
            MainForm.LogController.saveInLog("Importacion|" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|" + MainForm.UserName);

            panelProduct.Visible = false;

            listProducts.DataSource = null;
            listProducts.Visible = true;
            listProducts.Items.Clear();
            loadProducts();
            errorlbl.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            validateInputs();
            Product selectedProduct =(Product) listProducts.SelectedItem;

            Product newProduct = new Product();
            newProduct.Name = txtName.Text;
            newProduct.Description = txtDescription.Text;
            newProduct.NumberInStock = int.Parse(txtStock.Text);
            newProduct.RequiredPoints = int.Parse(txtPoints.Text);

            DesktopLogicController.updateProduct(selectedProduct, newProduct);

            listProducts.DataSource = null; 
            listProducts.Items.Clear();
            loadProducts();
            panelProduct.Visible = false;

        }


        private void validateInputs()
        {

            try
            {
                string name = txtName.Text;
                string description = txtDescription.Text;
                if (validateStringFields(name, description))
                {
                    int points = int.Parse(txtPoints.Text);
                    int stock = int.Parse(txtStock.Text);

                }
                else
                {
                    MessageBox.Show("Debe ingresar un nombre y una descripcion");
                }


            }
            catch (FormatException)
            {
                MessageBox.Show("Cantidad de puntos y stock deben ser numeros");
            }


        }

        private void clearInputs()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtPoints.Text = "";
            txtStock.Text = "";

        }

        private void deleteProductbtn_Click(object sender, EventArgs e)
        {
            
            Product product = (Product)listProducts.SelectedItem;
          
            if (product != null)
            {

                DesktopLogicController.deleteProduct(product);
                listProducts.DataSource = null;
                listProducts.Items.Clear();
                loadProducts();
                errorlbl.Text = "";

            }
            else
            {
                errorlbl.Text = "Debe seleccionar un producto a eliminar";
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(mainMenu);
        }
    }
    
}
