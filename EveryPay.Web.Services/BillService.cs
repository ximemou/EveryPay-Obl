using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;
using EveryPay.DTO;
using EveryPay.Exceptions;
using EveryPay.Validators;
using EveryPay.Enumerators;

namespace EveryPay.Web.Services
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public BillService()
        {
            unitOfWork = new UnitOfWork();
        }

        public int CreateBill(Bill aBill)
        {

            unitOfWork.BillRepository.Insert(aBill);
            unitOfWork.Save();
            return aBill.BillId;
        }

        public bool DeleteBill(int billId)
        {
            if (ExistsBill(billId))
            {
                Bill bill = unitOfWork.BillRepository.GetByID(billId);
                Transaction transaction = unitOfWork.TransactionRepository.GetByID(bill.TransactionId);
                if (!transaction.Pay)
                {
                    transaction.TotalAmount -= bill.Amount;
                    unitOfWork.BillRepository.Delete(billId);
                    unitOfWork.TransactionRepository.Update(transaction);
                    unitOfWork.Save();
                    return true;
                }
                else
                {
                    throw new TransactionAlreadyPayException("La transaccion a la cual pertenece la factura ya fue paga. No se puede eliminar");
                }
            }
            return false;
        }

        public IEnumerable<Bill> GetAllBills()
        {
            return unitOfWork.BillRepository.Get();
        }

        public Bill GetBillById(int billId)
        {
            return unitOfWork.BillRepository.GetByID(billId);
        }

        public bool UpdateBill(int billId, Bill aBill)
        {
            
            if (ExistsBill(billId))
            {
                Bill billToModify = unitOfWork.BillRepository.GetByID(billId);
                billToModify.Amount = aBill.Amount;
                unitOfWork.BillRepository.Update(billToModify);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
        private bool ExistsBill(int billId)
        {
            return unitOfWork.BillRepository.GetByID(billId) != null;
        }

        public bool AddValuesToBill(int billId, List<SpecificFieldValueDTO> values)
        {
            if (ExistsBill(billId))
            {
                if (!ValidateExistsBillForTheSupplier(billId, values))
                {
                    if (ValidateDoesntRepeatValuesForSupplierFields(values))
                    {
                        if (ValidateAllFieldsHaveValue(billId, values))
                        {

                            validateSpecificFieldValuesMatchesFieldType(values);
                            validateTansactionIsNotAlreadyRegistered(billId, values);

                            if (ValidateSpecificValuesAreForSameSupplier(billId, values))
                            {

                                foreach (SpecificFieldValueDTO specificFieldValueDTO in values)
                                {
                                    SpecificFieldValue specificFieldValue = ConvertDTO(specificFieldValueDTO);

                                    specificFieldValue.Bill = unitOfWork.BillRepository.GetByID(billId);

                                    unitOfWork.SpecificFieldValueRepository.Insert(specificFieldValue);

                                }
                                unitOfWork.Save();

                                return true;
                            }
                            else
                            {
                                throw new WrongDataTypeException("Los valores ingresados deben corresponder al mismo proveedor");
                            }

                        }
                        else
                        {
                            throw new WrongDataTypeException("Debe ingresar valores para todos los campos");
                        }
                    }
                    else
                    {
                        throw new WrongDataTypeException("Debe ingresar los valores para los distintos campos");
                    }

                }
                else
                {
                    throw new WrongDataTypeException("Datos mal ingresados. Verifique que exista una factura para los proveedores");
                }
            }

            return false;
        }


        private bool ValidateDoesntRepeatValuesForSupplierFields(List<SpecificFieldValueDTO> values)
        {
            bool ok = false;
            var duplicates = values.GroupBy(s => s.IdSupplierField)
                                                  .Where(g => g.Count() > 1)
                                                  .ToList();
            if (duplicates.Count == 0)
            {
                ok = true;
            }

            return ok;
        }


        private void validateSpecificFieldValuesMatchesFieldType(List<SpecificFieldValueDTO> values)
        {


            foreach (SpecificFieldValueDTO specificFieldValue in values)
            {
                if (unitOfWork.SupplierFieldRepository.GetByID(specificFieldValue.IdSupplierField) != null)
                {
                    ITypeValidator validator;
                    SupplierField supplierField = unitOfWork.SupplierFieldRepository.GetByID(specificFieldValue.IdSupplierField);
                    SupplierFieldType fieldType = (SupplierFieldType)Enum.Parse(typeof(SupplierFieldType), supplierField.TypeOfField, true);


                    switch (fieldType)
                    {
                        case (SupplierFieldType.Text):
                            {
                                break;
                            }
                        case (SupplierFieldType.Numeric):
                            {
                                validator = new NumericFieldValidator();
                                validator.validateTypeMatchesGivenValue(specificFieldValue.Value);
                                break;
                            }
                        case (SupplierFieldType.Date):
                            {
                                validator = new DateFieldValidator();
                                validator.validateTypeMatchesGivenValue(specificFieldValue.Value);
                                break;
                            }
                    }
                }
                else
                {
                    throw new WrongDataTypeException("Datos mal ingresados");
                }
            }
        }


        public bool ValidateExistsBillForTheSupplier(int billId, List<SpecificFieldValueDTO> values)
        {
            bool wrong = false;
            for (int i = 0; i < values.Count && !wrong; i++)
            {
                SpecificFieldValueDTO specificValue = values[i];

                SupplierField supplierField = unitOfWork.SupplierFieldRepository.GetByID(specificValue.IdSupplierField);
                Supplier supplier = unitOfWork.SupplierRepository.GetByID(supplierField.SupplierId);

                if (unitOfWork.BillSupplierRepository.Get(b => b.BillId == billId && b.SupplierId == supplier.SupplierId) == null)
                {
                    wrong = true;
                }
            }
            return wrong;
        }

        private SpecificFieldValue ConvertDTO(SpecificFieldValueDTO valueDTO)
        {
            SupplierField supplierField = unitOfWork.SupplierFieldRepository.Get(s => s.SupplierFieldId == valueDTO.IdSupplierField).FirstOrDefault();
            if (supplierField != null)
            {
                SpecificFieldValue specificValue = new SpecificFieldValue();
                specificValue.Value = valueDTO.Value;
                specificValue.Supplierfield = supplierField;
                return specificValue;
            }
            else
            {
                throw new NotFoundException("No existe el campo de proveedor especificado");
            }
        }
        private void validateTansactionIsNotAlreadyRegistered(int billId, List<SpecificFieldValueDTO> values)
        {
            bool matches = true;
            if (unitOfWork.SpecificFieldValueRepository.Get().Count() > 0)
            {
                Bill bill = unitOfWork.BillRepository.GetByID(billId);
                for (int i = 0; i < values.Count && matches; i++)
                {
                    SpecificFieldValueDTO specificFieldValue = values[i];
                    IEnumerable<SpecificFieldValue> possibleMatches = unitOfWork.SpecificFieldValueRepository.Get
                        (s => s.Value == specificFieldValue.Value && s.SupplierFieldId == specificFieldValue.IdSupplierField && s.Bill.Amount == bill.Amount);

                    if (possibleMatches.Count() == 0)
                    {
                        matches = false;
                    }

                }

                if (matches)
                {
                    throw new PaymentAlreadyRegisteredException("Ya existe una factura con esos datos");
                }
            }
        }
        private bool ValidateSpecificValuesAreForSameSupplier(int billId, List<SpecificFieldValueDTO> specificFieldValues)
        {
            bool theSame = true;
            Bill bill = unitOfWork.BillRepository.GetByID(billId);
            Supplier supplier = unitOfWork.SupplierRepository.GetByID(bill.Supplier.SupplierId);
            IEnumerable<SupplierField> supplierFields = unitOfWork.SupplierFieldRepository.Get(s => s.SupplierId == supplier.SupplierId);
            for (int i = 0; i < specificFieldValues.Count && !theSame; i++)
            {
                Supplier specificSupplier = unitOfWork.SupplierRepository.GetByID(specificFieldValues[0].IdSupplierField);
                if (!supplier.Equals(specificSupplier))
                {
                    theSame = false;
                }
            }
            return theSame;
        }
        private bool ValidateAllFieldsHaveValue(int billId, List<SpecificFieldValueDTO> values)
        {
            bool ok = true;

            int numberOfSpecificFieldValue = values.Count;

            Bill bill = unitOfWork.BillRepository.GetByID(billId);
            Supplier supplier = unitOfWork.SupplierRepository.GetByID(bill.Supplier.SupplierId);

            int numberOfFieldsItMustHave = unitOfWork.SupplierFieldRepository.Get(s => s.SupplierId == supplier.SupplierId).Count();

            if (numberOfFieldsItMustHave != numberOfSpecificFieldValue)
            {
                ok = false;
            }

            return ok;
        }

        public IEnumerable<SpecificFieldValue> GetAllFieldValuesForBill(int billId)
        {
            if (ExistsBill(billId))
            {
                return unitOfWork.SpecificFieldValueRepository.Get(s => s.BillId == billId);
            }
            else
            {
                throw new NotFoundException("La factura no se ha encontrado");
            }
        }

       
    }
}
