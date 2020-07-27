using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;
using EveryPay.DTO;
using EveryPay.Exceptions;
using EveryPay.Enumerators;

namespace EveryPay.Web.Services
{
    public class SupplierService : ISupplierService
    {

        private readonly IUnitOfWork unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public SupplierService()
        {
            unitOfWork = new UnitOfWork();
        }

        public int CreateSupplier( Supplier aSupplier)
        {
            if (ValidateAllFields(aSupplier))
            {
                if (ValidateCommission(aSupplier))
                {
                    if (ValidateSupplierNameIsUnique(aSupplier))
                    {
                        unitOfWork.SupplierRepository.Insert(aSupplier);
                        unitOfWork.Save();
                        return aSupplier.SupplierId;
                    }

                    else
                    {
                        throw new NotUniqueException("El nombre del proveedor ya existe");
                    }
                }else
                {
                    throw new InvalidCommissionException("Debe ingresar una comision mayor a 0");
                }
            }
            else
            {
                throw new NotEnoughDataException("Debe ingresar el nombre del proveedor");
            }
        }

        private bool ValidateAllFields( Supplier supplier)
        {
            if(supplier.Name!=null && supplier.Name.Length!=0)
            {
                return true;
            }
            return false;
        }

        private bool ValidateCommission(Supplier supplier)
        {

            bool commissionIsNaN = float.IsNaN(supplier.Commission);

            if (!commissionIsNaN && supplier.Commission > 0)
            {
                return true;
            }
            return false;


        }

        public bool DeleteSupplier( int supplierId)
        {
            if (ExistsSupplier(supplierId) )
            {
                Supplier supplier = unitOfWork.SupplierRepository.GetByID(supplierId);
                supplier.Delete = true;
                unitOfWork.SupplierRepository.Update(supplier);
                unitOfWork.Save();
                return true;
            }
            return false;
        }


        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return unitOfWork.SupplierRepository.Get(s => s.Delete == false);
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return unitOfWork.SupplierRepository.Get(s => s.SupplierId == supplierId &&  s.Delete == false).FirstOrDefault();
        }

        public bool UpdateSupplier( int supplierId, Supplier aSupplier)
        {
        
            if (ExistsSupplier(supplierId))
            {
                if (ValidateSupplierNameIsUniqueWhenUpdate(supplierId,aSupplier))
                {
                    if (ValidateCommission(aSupplier))
                    {
                        Supplier supplier = unitOfWork.SupplierRepository.GetByID(supplierId);
                       
                        supplier.Name = aSupplier.Name;
                        supplier.Commission = aSupplier.Commission;
                        unitOfWork.SupplierRepository.Update(supplier);
                        unitOfWork.Save();
                        return true;
                    }
                    else
                    {
                        throw new InvalidCommissionException("La comision debe ser un numero mayor a 0");
                    }
                }
                else
                {
                    throw new NotUniqueException("El nombre del proveedor ya existe");
                }
               
            }
            return false;
        }

        private bool ExistsSupplier(int supplierId)
        {
            bool exists = false;
            if( unitOfWork.SupplierRepository.Get(s => s.SupplierId == supplierId && s.Delete == false).FirstOrDefault() != null)
            {
                exists = true;
            }
            return exists;
        }

        public bool AddSupplierfields(int supplierId, List<SupplierField> supplierFields)
        {
            if (ExistsSupplier(supplierId))
            {

                if(validateSupplierFieldType(supplierFields))
                {
                    Supplier supplier = unitOfWork.SupplierRepository.GetByID(supplierId);
      
                    unitOfWork.CustomSupplierFieldRepository.AddFieldsToSupplier(supplier, supplierFields);
                    unitOfWork.Save();
                    return true;
                }
                else
                {
                    throw new WrongDataTypeException("El tipo de los campos de los proveedores esta mal ingresado");
                }
                
            }
            else
            {
                return false;
            }
        }

       

        private bool validateSupplierFieldType(List<SupplierField> supplierFields)
        {
            bool valid = true;

            for(int i =0; i<supplierFields.Count && valid; i++)
            {
                string supplierFieldType = supplierFields[i].TypeOfField;

                if (!Enum.IsDefined(typeof(SupplierFieldType), supplierFieldType))
                {
                    valid = false;
                }
            }
            return valid;
        }

        public bool UpdateSupplierField(int fieldId, SupplierField supplierField)
        {
            SupplierField supField = unitOfWork.SupplierFieldRepository.GetByID(supplierField);
            if (supField != null)
            {
                supField.FieldName = supplierField.FieldName;
                return true;
            }
            else
            {
                return false;
            }


        }


        private bool ValidateSupplierNameIsUniqueWhenUpdate(int supplierId,Supplier supplier)
        {
            return unitOfWork.SupplierRepository.Get(s => s.Name == supplier.Name && s.Delete == false && s.SupplierId!=supplierId).FirstOrDefault() == null;

        }

        private bool ValidateSupplierNameIsUnique(Supplier supplier)
        {
            return unitOfWork.SupplierRepository.Get(s => s.Name == supplier.Name && s.Delete == false).FirstOrDefault() == null;
            
        }

        public IEnumerable<SupplierField> GetSuppliersFields(int supplierId)
        {
            if (ExistsSupplier(supplierId))
            {
                return unitOfWork.CustomSupplierFieldRepository.GetAllFieldsFromSupplier(supplierId);
            }
            else
            {
                throw new NotFoundException("El proveedor no se ha encontrado");
            }
        }

        public bool DeleteSupplierField(int supplierId, int fieldId)
        {
            if (ExistsSupplier(supplierId))
            {
                if (ExistsSupplierField(fieldId))
                {
                    unitOfWork.CustomSupplierFieldRepository.Delete(fieldId);
                    unitOfWork.Save();
                    return true;
                }
                return false;

            }
            else
            {
                return false;
            }
            
        }

        public bool ExistsSupplierField(int fieldId)
        {
            return unitOfWork.CustomSupplierFieldRepository.GetByID(fieldId)!=null;
        }

        public bool suppliersFieldsValidation(List<SupplierField> fields)
        {
            if (fields.Count>0 &&validateSupplierFieldType(fields) && !ValidateDoesntRepeatSuplierFields(fields) )
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public bool ValidateDoesntRepeatSuplierFields(List<SupplierField> supplierFields)
        {
            bool notOk = false;
            for (int i = 0; i < supplierFields.Count && !notOk; i++)
            {
                for (int j = i ; j < supplierFields.Count && !notOk; j++)
                {

                    if (i!=j && supplierFields[i].FieldName == supplierFields[j].FieldName)
                    {
                        notOk = true;
                    }
                }
            }
            return notOk;
        }
    }
}
