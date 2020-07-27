using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveryPay.Desktop.ImportInterface
{
    public interface IProductsImporter
    {
        UserControl Panel();
        List<ProductDTO> GetProducts(List<string> inputs);

    }

}
