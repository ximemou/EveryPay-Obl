using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredPoints { get; set; }
        public int NumberInStock { get; set; }
        public Product() { }

        public override string ToString()
        {
            return Name + "- En stock: " + NumberInStock;
               
        }
    }
}
