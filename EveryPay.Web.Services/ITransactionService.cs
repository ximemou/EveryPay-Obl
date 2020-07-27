using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.DTO;

namespace EveryPay.Web.Services
{
    public interface ITransactionService
    {
        Transaction GetTransactionById(int transactionId);
        IEnumerable<Transaction> GetAllTransactions();
        int CreateTransaction( TransactionDTO aTransaction);
        bool DeleteTransaction( int transactionId);
        bool AddBillsToTtransaction(int transactionId, List<BillDTO> bills);
        bool PayTransaction(PaymentDTO paymentMethod,int transactionId);
    }
}
