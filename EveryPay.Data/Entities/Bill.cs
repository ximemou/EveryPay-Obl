using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public float Amount { get; set; }  
        public virtual List<SupplierField> SupplierFields { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int TransactionId { get; set; }
        public Bill()
        {
            SupplierFields = new List<SupplierField>();
        }
    }
}
