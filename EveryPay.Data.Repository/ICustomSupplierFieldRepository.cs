using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;

namespace EveryPay.Data.Repository
{
    public interface ICustomSupplierFieldRepository: IRepository<SupplierField>
    {
        void AddFieldsToSupplier(Supplier supplier, List<SupplierField> supplierFields);
        IEnumerable<SupplierField> GetAllFieldsFromSupplier(int supplierId);
    }
}
