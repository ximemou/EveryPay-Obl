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
    public partial class PointsVariablePanel : UserControl
    {
        public MainForm MainForm { get; set; }
        private DesktopLogicController DesktopLogicController { get; set; }
        public PointsVariablePanel(MainForm main)
        {
            InitializeComponent();
            DesktopLogicController = new DesktopLogicController();
            MainForm = main;
            loadVariablePoints();

        }

        private void loadVariablePoints()
        {
             SystemSettings settings=DesktopLogicController.getSettings() ;
             textBoxPoints.Text = ""+settings.MoneyForPoint;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(mainMenu);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                
                int money=int.Parse(textBoxPoints.Text);

                if (money > 0)
                {   
                   DesktopLogicController.setSettings(money);
                    textBoxPoints.Text = "" + money;
                }else
                {
                    MessageBox.Show("La variable de puntos debe ser un numero positivo");
                }               
            }
            catch(FormatException)
            {
                MessageBox.Show("La variable de puntos debe ser un numero entero");
            }
        }
    }
}
