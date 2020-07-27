using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveryPay.Desktop.WindowsFormApp
{
    public partial class MainMenuPanel : UserControl
    {
        public MainForm MainForm { get; set; }
        public MainMenuPanel(MainForm main)
        {
            InitializeComponent();
            MainForm = main;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            LogInPanel login = new LogInPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(login);
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            LogPanel logMenu = new LogPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(logMenu);
        }

        private void btnImportProducts_Click(object sender, EventArgs e)
        {
            ImportProductsPanel importMenu = new ImportProductsPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(importMenu);
        }

        private void btnSystemVriables_Click(object sender, EventArgs e)
        {
            SuppliersBlackListPanel suppliersMenu = new SuppliersBlackListPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(suppliersMenu);
        }

        private void btnPointsVariable_Click(object sender, EventArgs e)
        {
            PointsVariablePanel pointsMenu = new PointsVariablePanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(pointsMenu);
        }

        private void adminProductsbtn_Click(object sender, EventArgs e)
        {
            ProductAdministrationPanel panel = new ProductAdministrationPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(panel);
        }
    }
}
