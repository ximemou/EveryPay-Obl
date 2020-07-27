using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Repository;
using EveryPay.Desktop.ImportInterface;

namespace EveryPay.Desktop.LogicController
{
    public class DesktopLogicController
    {
        private readonly IUnitOfWork unitOfWork; 
        public DesktopLogicController()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Supplier> getSuppliers()
        {
            IEnumerable<Supplier> suppliersAsIEnumerable = unitOfWork.SupplierRepository.Get(s => s.Delete == false);
            return suppliersAsIEnumerable.ToList();
        }

        public void addSupplierToBlackList(int supplierId)
        {
            Supplier toModify = unitOfWork.SupplierRepository.GetByID(supplierId);
            toModify.InBlackList = true;
            unitOfWork.SupplierRepository.Update(toModify);
            unitOfWork.Save();
        }

        public void removeSupplierFromBlackList(int supplierId)
        {
            Supplier toModify = unitOfWork.SupplierRepository.GetByID(supplierId);
            toModify.InBlackList = false;
            unitOfWork.SupplierRepository.Update(toModify);
            unitOfWork.Save();
        }

       public SystemSettings getSettings()
        {
           return  unitOfWork.SettingsRepository.Get().FirstOrDefault();
        }

        public void setSettings(int money)
        {
            SystemSettings settings = unitOfWork.SettingsRepository.Get().FirstOrDefault();
            settings.MoneyForPoint = money;
            unitOfWork.SettingsRepository.Update(settings);
            unitOfWork.Save();
        }

        public void addProductsToDatabase(List<ProductDTO> productDtoList)
        {
            ConvertProductDto converter = new ConvertProductDto();    

            foreach (ProductDTO productDto in productDtoList)
            {
               Product product= converter.ConvertDto(productDto);

                unitOfWork.ProductRepository.Insert(product);

            }
            unitOfWork.Save();

        }

        public List<Product> getAllProducts()
        {
           return unitOfWork.ProductRepository.Get().ToList();

        }

        public void saveProduct(Product product)
        {
            unitOfWork.ProductRepository.Insert(product);
            unitOfWork.Save();
        }

        public void updateProduct(Product existingProduct,Product newProduct)
        {

            existingProduct.Name = newProduct.Name;
            existingProduct.Description = newProduct.Description;
            existingProduct.RequiredPoints = newProduct.RequiredPoints;
            existingProduct.NumberInStock = newProduct.NumberInStock;

            unitOfWork.ProductRepository.Update(existingProduct);
            unitOfWork.Save();


        }

        public void deleteProduct(Product product)
        {
            unitOfWork.ProductRepository.Delete(product);
            unitOfWork.Save();
        }
    }
}
