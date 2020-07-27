using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Desktop.ImportInterface
{
   public class ProductDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredPoints { get; set; }
        public int NumberInStock { get; set; }


    }
}
