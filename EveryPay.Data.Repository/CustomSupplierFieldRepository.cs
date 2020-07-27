using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.DataAccess;
using System.Data.Entity;
using EveryPay.Exceptions;

namespace EveryPay.Data.Repository
{
    public class CustomSupplierFieldRepository : ICustomSupplierFieldRepository
    {
        internal IRepository<SupplierField> GenericRepository;
        internal EveryPayContext context;
        internal DbSet<SupplierField> dbSet;

        public CustomSupplierFieldRepository(EveryPayContext context)
        {
            this.context = context;
            this.dbSet = context.Set<SupplierField>();
            GenericRepository = new GenericRepository<SupplierField>(context);
        }


        public void AddFieldsToSupplier(Supplier supplier, List<SupplierField> supplierFields)
        {
            if (!ValidateUniqueSuplierFieldsForSupplier(supplier, supplierFields) && !ValidateDoesntRepeatSuplierFields(supplierFields))
            {
                foreach (var field in supplierFields)
                {
                        field.SupplierId = supplier.SupplierId;
                        Insert(field);
                }
            }
            else
            {
                throw new NotUniqueException("El proveedor no puede tener campos particulares con el mismo nombre");
            }
        }

        public bool ValidateDoesntRepeatSuplierFields(List<SupplierField> supplierFields)
        {
            bool notOk = false;
            for (int i = 0; i < supplierFields.Count && !notOk; i++)
            {
                for (int j = i; j < supplierFields.Count && !notOk; j++)
                {

                    if (i != j && supplierFields[i].FieldName == supplierFields[j].FieldName)
                    {
                        notOk = true;
                    }
                }
            }
            return notOk;
        }


        public bool ValidateUniqueSuplierFieldsForSupplier(Supplier supplier,List<SupplierField> supplierFields)
        {
            bool notOk = false;
            for(int i = 0; i < supplierFields.Count && !notOk ; i++)
            {
                SupplierField field = supplierFields[i];
                if(GenericRepository.Get(s => s.FieldName == field.FieldName && s.SupplierId == supplier.SupplierId).FirstOrDefault() != null)
                {
                    notOk = true;
                }
            }
            return notOk;
        }


        public IEnumerable<SupplierField> GetAllFieldsFromSupplier(int supplierId)
        {
            return GenericRepository.Get(sf => sf.SupplierId == supplierId);
        }

        /*****************IMPLEMENTED METHODS FROM IREPOSITORY*************************/

        public void Delete(SupplierField entityToDelete)
        {
            GenericRepository.Delete(entityToDelete);
        }

        public void Delete(object id)
        {
            GenericRepository.Delete(id);
        }
    
        public IEnumerable<SupplierField> Get(Expression<Func<SupplierField, bool>> filter = null, Func<IQueryable<SupplierField>, IOrderedQueryable<SupplierField>> orderBy = null, string includeProperties = "")
        {
            return GenericRepository.Get();
        }
   
        public SupplierField GetByID(object id)
        {
            return GenericRepository.GetByID(id);
        }

        public void Insert(SupplierField entityToCreate)
        {
            GenericRepository.Insert(entityToCreate);
        }

        public void Update(SupplierField entityToUpdate)
        {
            GenericRepository.Update(entityToUpdate);
        }

        public SupplierField GetSupplierField(int supplierFieldId)
        {

            return GenericRepository.GetByID(supplierFieldId);           
            
        }
    }
}
