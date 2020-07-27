using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveryPay.Desktop.LogInterface;

namespace EveryPay.Desktop.WindowsFormApp
{
    public partial class MainForm : Form
    {
        public Panel WindowPanel { get; set; }

        public ILogController LogController { get; set; }

        public string UserName { get; set; }
        public MainForm(ILogController logController)
        {
            InitializeComponent();
            WindowPanel = windowPanel;
            LogController = logController;
            initializeWindow();


        }

        private void initializeWindow()
        {
            LogInPanel login = new LogInPanel(this);
            emptyMainFormPanel();
            WindowPanel.Controls.Add(login);
        }

        public void emptyMainFormPanel()
        {
            if (!panelIsEmpty())
            {         
                WindowPanel.Controls.Clear();
            }
        
        }

        private bool panelIsEmpty()
        {
            return windowPanel.Controls.Count == 0;
        }
    }
}
