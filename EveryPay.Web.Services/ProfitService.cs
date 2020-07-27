using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;

namespace EveryPay.Web.Services
{
    public class ProfitService : IProfitService
    {

        private readonly IUnitOfWork unitOfWork;
        private Dictionary<string, float> earnings;

        public ProfitService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            earnings = new Dictionary<string, float>();
            InitializeErningsDictionary();

        }

        public ProfitService()
        {
            unitOfWork = new UnitOfWork();
            earnings = new Dictionary<string, float>();
            InitializeErningsDictionary();
        }

        public Dictionary<string, float> GetProfitsForEachSupplier(string [] dates)
        {

            DateTime startingDate = ConvertStringToDate(dates[0]);

            DateTime endingDate = ConvertStringToDate(dates[1]);

            IEnumerable<Transaction> transactions = unitOfWork.TransactionRepository.Get(tr =>tr.TransactionDate >= startingDate && tr.TransactionDate <= endingDate);

            foreach (Transaction transaction in transactions)
            {
                if (transaction.Pay)
                {
                    IEnumerable<Bill> bills = unitOfWork.BillRepository.Get(b => b.TransactionId == transaction.TransactionId);

                    foreach (Bill bill in bills)
                    {
                        Supplier supplier = unitOfWork.SupplierRepository.Get(s => s.SupplierId == bill.Supplier.SupplierId).First();

                        earnings[supplier.Name] += (supplier.Commission * bill.Amount) / 100;

                    }
                }
            }

            return earnings;
        }

        public float GetProfitsInPeriod(string [] dates)
        {

            float totalEarnings = 0;

            DateTime startingDate = ConvertStringToDate(dates[0]);

            DateTime endingDate = ConvertStringToDate(dates[1]);

            IEnumerable<Transaction> transactions = unitOfWork.TransactionRepository.Get(tr => tr.TransactionDate >= startingDate && tr.TransactionDate <= endingDate);

            foreach(Transaction transaction in transactions)
            {
                if (transaction.Pay)
                {
                    IEnumerable<Bill> bills = unitOfWork.BillRepository.Get(b => b.TransactionId == transaction.TransactionId);


                    foreach (Bill bill in bills)
                    {
                        Supplier supplier = unitOfWork.SupplierRepository.Get(s => s.SupplierId == bill.Supplier.SupplierId).First();

                        totalEarnings += (supplier.Commission * bill.Amount) / 100;

                    }
                }
            }

            return totalEarnings;
      
        }

        private void InitializeErningsDictionary()
        {
            IEnumerable<Supplier> suppliers = unitOfWork.SupplierRepository.Get();

            foreach (Supplier supplier in suppliers)
            {
                earnings.Add(supplier.Name, 0);
            }

        }

        private DateTime ConvertStringToDate(string date)
        {
           return DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
