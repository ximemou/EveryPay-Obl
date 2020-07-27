using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EveryPay.Data.Entities
{
    public  class SupplierField
    {
        [Key]
        public int SupplierFieldId { get; set; }
        public string FieldName { get; set; }  
        public int SupplierId { get; set; }
        public string TypeOfField { get; set; }

    }
}
