using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Desktop.ImportInterface;

namespace EveryPay.Desktop.LogicController
{
   public class ConvertProductDto
    {

        public Product ConvertDto(ProductDTO productDto)
        {
            Product product = new Product();
            product.Description = productDto.Description;
            product.Name = productDto.Name;
            product.NumberInStock = productDto.NumberInStock;
            product.RequiredPoints = productDto.RequiredPoints;
            return product;

        }
    }
}
