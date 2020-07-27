using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveryPay.Desktop.LogInterface;

namespace EveryPay.Desktop.WindowsFormApp
{
    public partial class LogPanel : UserControl
    {
        public MainForm MainForm { get; set; }
        public LogPanel(MainForm main)
        {
            InitializeComponent();
            MainForm = main;
            initializeLogGrid();

            dateFrom.CustomFormat = "yyyy-MM-dd HH:mm:ss tt";
            dateFrom.Format = DateTimePickerFormat.Custom;

            dateTo.CustomFormat = "yyyy-MM-dd HH:mm:ss tt";
            dateTo.Format = DateTimePickerFormat.Custom;


        }

        private void initializeLogGrid()
        {
            List<LogEntity> allLogs = MainForm.LogController.displayLog();
            LogGrid.DataSource = allLogs;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            MainMenuPanel mainMenu = new MainMenuPanel(MainForm);
            MainForm.emptyMainFormPanel();
            MainForm.WindowPanel.Controls.Add(mainMenu);
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            DateTime from = dateFrom.Value;
            DateTime to = dateTo.Value;

            List<LogEntity> filteredLogs = MainForm.LogController.displayLogBetweenDates(from, to);
            LogGrid.DataSource = filteredLogs;            
        }

        private void LogGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogGrid.Rows[e.RowIndex].ReadOnly = true;

            }
            catch (ArgumentOutOfRangeException) { }
        }
    }
}
