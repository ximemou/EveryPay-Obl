using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.DTO;

namespace EveryPay.Web.Services
{
    public interface ISupplierService
    {
        Supplier GetSupplierById(int supplierId);
        IEnumerable<Supplier> GetAllSuppliers();
        int CreateSupplier( Supplier aSupplier);
        bool UpdateSupplier( int supplierId, Supplier aSupplier);
        bool DeleteSupplier( int supplierId);
        bool AddSupplierfields(int supplierId, List<SupplierField> supplierFields);
        bool UpdateSupplierField(int fieldId, SupplierField supplierField);
        IEnumerable<SupplierField> GetSuppliersFields(int supplierId);
        bool DeleteSupplierField(int supplierId, int fieldId);

        bool suppliersFieldsValidation(List<SupplierField> fields);
    }
}
