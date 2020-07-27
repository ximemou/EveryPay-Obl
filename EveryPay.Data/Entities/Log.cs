using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }

        public Log(string action, DateTime date, string userName)
        {
            Action = action;
            Date = date;
            UserName = userName;
        }
        public Log()
        {

        }
    }
}
