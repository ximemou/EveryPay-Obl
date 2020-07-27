using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Desktop.LogInterface
{
    public class LogEntity
    {
        public string ActionType { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        public LogEntity(LogAction action, DateTime date, string userName)
        {
            ActionType = action.ToString();
            Date = date;
            UserName = userName;
        }
    }
}
