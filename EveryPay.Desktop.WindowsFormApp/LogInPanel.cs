using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveryPay.Desktop.LoginManager;

namespace EveryPay.Desktop.WindowsFormApp
{
    public partial class LogInPanel : UserControl
    {
        public MainForm MainForm { get; set; }
        public LogInPanel(MainForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            passwordInput.PasswordChar = '*';
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string userName = usernameInput.Text;
            string password = passwordInput.Text;

            LoginValidator validator = new LoginValidator(userName, password);
            try
            {
                validator.validateLogin();

                MainForm.LogController.saveInLog("Ingreso|" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|" + userName);
                MainForm.UserName = userName;
                MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
                MainForm.emptyMainFormPanel();
                MainForm.WindowPanel.Controls.Add(mainMenu);
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
            }
        }
    }
}
