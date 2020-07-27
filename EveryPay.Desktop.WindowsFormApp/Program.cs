using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EveryPay.Desktop.LogInterface;
using EveryPay.Desktop.LogicController;
using EveryPay.LogManager;

namespace EveryPay.Desktop.WindowsFormApp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ILogController logController = new LogController();
            Application.Run(new MainForm(logController));
        }
    }
}
