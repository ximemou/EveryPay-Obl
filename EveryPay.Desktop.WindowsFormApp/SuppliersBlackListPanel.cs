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
    public partial class SuppliersBlackListPanel : UserControl
    {
        public MainForm MainForm { get; set; }
        public SuppliersBlackListPanel(MainForm main)
        {
            InitializeComponent();
            MainForm = main;
            initializeSuppliersListBox();
        }

        private void initializeSuppliersListBox()
        {
            DesktopLogicController logic = new DesktopLogicController();
            List<Supplier> allSuppliers = logic.getSuppliers();
            listBoxSuppliers.DataSource = allSuppliers;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(mainMenu);
        }

        private void listBoxSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Supplier supplier = (Supplier) listBoxSuppliers.SelectedItem;
            if (supplier.InBlackList)
            {
                addOrRemoveBtn.Text = "Quitar de la lista negra";
            }
            else
            {
                addOrRemoveBtn.Text = "Agregar a lista negra";
            }
        }

        private void addOrRemoveBtn_Click(object sender, EventArgs e)
        {
            Supplier supplier = (Supplier)listBoxSuppliers.SelectedItem;
            DesktopLogicController logic = new DesktopLogicController();

            if (supplier.InBlackList)
            {
                logic.removeSupplierFromBlackList(supplier.SupplierId);
            }
            else
            {
                logic.addSupplierToBlackList(supplier.SupplierId);
            }
            initializeSuppliersListBox();
        }
    }
}
