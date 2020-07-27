using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public float TotalAmount { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public string PayMethod { get; set; }
        public bool Pay { get; set; }
        public virtual Client Client { get; set; }

        public int? ClientId { get; set; }

        public Transaction()
        {
            Bills = new List<Bill>();
           
            Pay = false;

            PayMethod = "not pay";
        }


    }
}
