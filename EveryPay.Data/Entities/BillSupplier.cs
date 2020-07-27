using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class BillSupplier
    {
        public int BillId { get; set; }
        public int SupplierId { get; set; }
        public virtual IEnumerable<Bill> Bills { get; set; }
        public virtual IEnumerable<SupplierField> SupplierField { get; set; }
    }
}
