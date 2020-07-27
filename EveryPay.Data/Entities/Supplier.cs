using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public float Commission { get; set; }
        public virtual List<SupplierField> SupplierFields { get; set; }  
        public bool Delete { get; set;}   
        public bool InBlackList { get; set; }
        public Supplier()
        {

            SupplierFields = new List<SupplierField>();
            Delete = false;
            Name = "";
            Commission = 0;
            InBlackList = false;

        }

        public Supplier(int id)
        {
            SupplierId = id;
            Delete = false;
            InBlackList = false;
        }

        public override string ToString()
        {
            string isInBlackList = InBlackList ? " (En lista negra)" : "";
            return Name + isInBlackList;
        }
    }
}
