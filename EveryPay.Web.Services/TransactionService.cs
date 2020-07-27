using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;
using EveryPay.DTO;
using EveryPay.Exceptions;
using EveryPay.Payment;

namespace EveryPay.Web.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly IUnitOfWork unitOfWork;
        private SystemSettings systemSettings;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.systemSettings = new SystemSettings();
        }

        public TransactionService()
        {
            unitOfWork = new UnitOfWork();
            this.systemSettings = new SystemSettings();
        }

        public int CreateTransaction( TransactionDTO aTransaction)
        {
            Transaction transaction = ConvertDTO(aTransaction);

            unitOfWork.TransactionRepository.Insert(transaction);
            unitOfWork.Save();
            return transaction.TransactionId;
        }

        public bool DeleteTransaction( int transactionId)
        {
            if (ExistsTransaction(transactionId))
            {
                unitOfWork.TransactionRepository.Delete(transactionId);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return unitOfWork.TransactionRepository.Get();
        }

        public Transaction GetTransactionById(int transactionId)
        {
            return unitOfWork.TransactionRepository.GetByID(transactionId);
        }
        private bool ExistsTransaction(int transactionId)
        {
            return unitOfWork.TransactionRepository.GetByID(transactionId) != null;
        }

        public bool AddBillsToTtransaction(int transactionId, List<BillDTO> bills)
        {
            if (ExistsTransaction(transactionId))
            {
                if (!unitOfWork.TransactionRepository.GetByID(transactionId).Pay)
                {
                    List<Bill> billsConverted = new List<Bill>();
                    foreach (BillDTO billDTO in bills)
                    {

                        ConvertBillDTO convert = new ConvertBillDTO();
                        Bill bill = convert.convertDTO(billDTO);

                        if (unitOfWork.SupplierRepository.Get(s => s.SupplierId == bill.Supplier.SupplierId && s.Delete == false).FirstOrDefault() != null)
                        {
                            if (ValidateAmount(bill))
                            {
                                validateSupplerHasFields(bill);
                                
                                bill.Supplier = unitOfWork.SupplierRepository.GetByID(bill.Supplier.SupplierId);
                                billsConverted.Add(bill);
                            }
                            else
                            {
                                throw new InvalidAmountException("El monto de la factura debe ser positivo");
                            }
                           
                        }
                        else
                        {
                            throw new NotFoundException("Datos de las facturas incorrectos");
                        }
                    }
                    Transaction transaction = unitOfWork.TransactionRepository.GetByID(transactionId);

                    foreach (Bill bill in billsConverted)
                    {

                        transaction.Bills.Add(bill);
                        transaction.TotalAmount += bill.Amount;

                    }

                    unitOfWork.Save();
                    AddToDataBase(transaction.TransactionId);
                    return true;
                }
                else
                {
                    throw new TransactionAlreadyPayException("No se puede agregar facturas a una transaccion que ya fue paga");
                }
            }
            else
            {
                return false;
            }

        }
        public bool ValidateAmount(Bill aBill)
        {
            return aBill.Amount > 0;
        }
        private void validateSupplerHasFields(Bill bill)
        {
            IEnumerable<SupplierField> supplierfields = unitOfWork.SupplierFieldRepository.Get(s => s.SupplierId == bill.Supplier.SupplierId);
            if (supplierfields.Count() == 0)
            {
                throw new NoSupplierFieldsException("El proveedor no posee campos por lo que no se puede crear una factura");
            }
        }

        private Transaction ConvertDTO(TransactionDTO transactionDto)
        {
            try
            {
                Transaction transaction = new Transaction();

                DateTime transactionDate = DateTime.ParseExact(transactionDto.TransactionDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                transaction.TransactionDate = transactionDate;

                IEnumerable<Client> clientOfTransaction = unitOfWork.ClientRepository.Get(c => c.Identification == transactionDto.ClientIdentification);
                if(clientOfTransaction.Count() == 1)
                {
                    transaction.ClientId = clientOfTransaction.FirstOrDefault().ClientId;
                }
                else
                {
                    transaction.ClientId = null;
                }
  
                return transaction;

            }
            catch
            {
                throw new DateFormatException("El formato de la fecha debe ser yyyy-MM-dd");
            }
          
        }
        private void AddToDataBase(int transactionId)
        {
            try
            {
                Transaction transaction = unitOfWork.TransactionRepository.GetByID(transactionId);
                Bill billToUpdate = unitOfWork.BillRepository.Get(b => b.TransactionId == transaction.TransactionId).Last();

                BillSupplier billSupplier = new BillSupplier();
                billSupplier.BillId = billToUpdate.BillId;
                billSupplier.SupplierId = billToUpdate.Supplier.SupplierId;
                unitOfWork.BillSupplierRepository.Insert(billSupplier);

                unitOfWork.Save();
            }
            catch(Exception )
            {
                throw new Exception("Ya existe una factura para el proveedor ingresado en la transaccion");
            }
        }

        public bool AmountGivenIsEnoughToPay(float amountGiven,float amountToPay)
        {
            return amountGiven >= amountToPay;
        }

        public bool PayTransaction(PaymentDTO paymentMethodDTO,int transactionId)
        {
            if (ExistsTransaction(transactionId))
            {
                Transaction transaction = unitOfWork.TransactionRepository.GetByID(transactionId);

                if (!transaction.Pay)
                {
                    validateSupplierHasFieldsToCompleteTransaction(transaction);

                    if (transaction.TotalAmount > 0)
                    {
                        validateBillHasSpecificValues(transaction);
                        ConvertPaymentMethod convert = new ConvertPaymentMethod();
                        IPaymentMethod paymentMethod = convert.convertPaymentDTO(paymentMethodDTO);
                        if (paymentMethod.PayTransaction(paymentMethodDTO.AmountGiven, transaction.TotalAmount))
                        {
                           
                            transaction.PayMethod = paymentMethod.GetType().Name;
                            transaction.Pay = true;
                            setPointsToClient(transaction);
                            unitOfWork.TransactionRepository.Update(transaction);

                            unitOfWork.Save();
                            return true;
                        }
                        return false;

                    }
                    else
                    {
                        throw new NonExistingPaymentException("La transaccion no tiene facturas asociadas por lo que no hay nada para pagar");
                    }
                }
                else
                {
                    throw new TransactionAlreadyPayException("La transaccion ya fue pagada");
                }
            }
            else
            {
                throw new NotFoundException("No existe la transaccion especificada");
            }

        }

        private void validateBillHasSpecificValues(Transaction transaction)
        {
           
            for(int i=0;i<transaction.Bills.Count; i++)
            {
                Bill bill = transaction.Bills[i];
                IEnumerable<SpecificFieldValue> SpecificFieldValue = unitOfWork.SpecificFieldValueRepository.Get(s => s.BillId == bill.BillId);
                if (SpecificFieldValue.Count() == 0)
                {
                    throw new NoSpecificValuesInBillException("Todos los campos de la factura deben tener un valor asociado");
                }
            }
        }

        private void validateSupplierHasFieldsToCompleteTransaction(Transaction transaction)
        {
            for (int i = 0; i < transaction.Bills.Count; i++)
            {
                Bill bill = transaction.Bills[i];
                IEnumerable<SupplierField> supplierField = unitOfWork.SupplierFieldRepository.Get(s => s.SupplierId == bill.Supplier.SupplierId);
                if (supplierField.Count() == 0)
                {
                    throw new NoSupplierFieldsException("El proveedor de la factura no posee campos. No se puede realizar el pago");
                }
            }
        }


        private void setPointsToClient(Transaction transaction)
        {
            if(transaction.ClientId != null)
            {
                int points = 0;

                List<Bill> bills = transaction.Bills;
                for (int i = 0; i < bills.Count; i++)
                {
                    float billAmount = bills[i].Amount;
                    Supplier supplier = bills[i].Supplier;
                    if (!supplier.InBlackList)
                    {
                        points += (int)(billAmount / systemSettings.MoneyForPoint);

                    }


                }
                Client client = transaction.Client;
                client.TotalPoints += points;
                unitOfWork.ClientRepository.Update(client);
                unitOfWork.Save();
            }
        }

    }
}
