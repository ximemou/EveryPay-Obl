using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
   public class SpecificFieldValue
    {
        [Key]
        public int SpecificFieldValueId { get; set; }    
        public virtual SupplierField Supplierfield { get; set; }
        public int SupplierFieldId { get; set; }
        public virtual Bill Bill { get; set; }
        public int BillId { get; set; }
        public string Value { get; set; }

    }
}
