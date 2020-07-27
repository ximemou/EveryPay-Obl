using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveryPay.Desktop.LogicController.Reflection;
using System.IO;
using EveryPay.Desktop.ImportInterface;
using EveryPay.Desktop.LogicController;

namespace EveryPay.Desktop.WindowsFormApp
{
    public partial class ImportProductsPanel : UserControl
    {
        public MainForm MainForm { get; set; }
        private IProductsImporter ProductsInterface { get; set; }
        public UserControl InputsPanel { get; set; }
        public ImportProductsPanel(MainForm main)
        {
            InitializeComponent();
            textDllsRoute.Text = Directory.GetCurrentDirectory() + "\\importDlls";
            MainForm = main;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(mainMenu);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string fileSystemRoute = textDllsRoute.Text;
            fillDllsListBox(fileSystemRoute);          
        }

        private void fillDllsListBox(string fileSystemRoute)
        {
            ReflectionHandler handler = new ReflectionHandler(fileSystemRoute);
            List<string> dlls = handler.getMatchingDlls();
            List<string> shortNameDll = getShortDllNameFromPath(dlls);
            listDlls.DataSource = shortNameDll;
            
        }

        private List<string> getShortDllNameFromPath(List<string> dlls)
        {
            List<string> shortNames = new List<string>();
            foreach(string dllFile in dlls)
            {
                string[] routeSplitted = dllFile.Split('\\');
                shortNames.Add(routeSplitted[routeSplitted.Length - 1]);
            }
            return shortNames;
        }

        private void btnSelectDll_Click(object sender, EventArgs e)
        {
            try
            {

                string selectedPath = (string)listDlls.SelectedItem;
                ReflectionHandler reflectionHandler = new ReflectionHandler(textDllsRoute.Text + "\\" + selectedPath);
                ProductsInterface = reflectionHandler.getInterfaceInstance();
                loadProductPanel();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Debe seleccionar la dll a utilizar");
            }
          
        }
        private void loadProductPanel()
        {
            UserControl inputPanel = ProductsInterface.Panel();
            InputsPanel = inputPanel;
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(inputPanel);
            MainForm.WindowPanel.Controls.Add(createImportProductsButton());
            MainForm.WindowPanel.Controls.Add(createBackButton());

        }

        private Control createBackButton()
        {
            Button button = new Button();
            button.Location = new Point(14, 455);
            button.Size = new System.Drawing.Size(75, 23);
            button.Text = "<-";

            button.Click += new EventHandler(this.backEventHandler);

            return button;
        }

        private void backEventHandler(object sender, EventArgs e)
        {
            MainForm.emptyMainFormPanel();
            ImportProductsPanel importProductsPanel = new ImportProductsPanel(MainForm);

            MainForm.WindowPanel.Controls.Add(importProductsPanel);
        }

        private void LoadProductToDatabase()
        {
           

                List<ProductDTO> productDtoList = ProductsInterface.GetProducts(fillInputs());
                DesktopLogicController logicController = new DesktopLogicController();
                logicController.addProductsToDatabase(productDtoList);

                MainForm.LogController.saveInLog("Importacion|" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|" + MainForm.UserName);
            
          

        }

       private List<string> fillInputs()
        {
            List<string> inputs = new List<string>();

            List<Control> list = InputsPanel.Controls.OfType<TextBox>().Cast<Control>().ToList();

            foreach(Control control in list)
            {
                string input = control.Text;
                inputs.Add(input);
            }

            return reverseList(inputs);
        }

        private List<string> reverseList(List<string> inputs)
        {
            List<string> reversedInputs = new List<string>();

            for (int i=inputs.Count -1; i >= 0; i--)
            {
                reversedInputs.Add(inputs[i]);
            }
            return reversedInputs;
        }

        private Button createImportProductsButton()
        {

            Button button = new Button();
            button.Location = new Point(500, 380);
            button.Size = new System.Drawing.Size(332, 50); 
            button.Text = "Importar productos";

            button.Click += new EventHandler(this.myButtonHandler);

            return button;


        }

        private void myButtonHandler(object sender, EventArgs e)
        {
            try
            {
                LoadProductToDatabase();
                MessageBox.Show("Productos agregados correctamente");

                MainForm.emptyMainFormPanel();
                MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
                
                MainForm.WindowPanel.Controls.Add(mainMenu);

                
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No existe la ruta especificada");
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo ingresar los prodcutos. Verifique el formato.");
            }
        }
    }
}
