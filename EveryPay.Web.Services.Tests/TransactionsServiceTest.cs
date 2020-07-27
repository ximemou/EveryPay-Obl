using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EveryPay.Data.Repository;
using System.Collections.Generic;
using EveryPay.Data.Entities;
using EveryPay.DTO;

namespace EveryPay.Web.Services.Tests
{
    [TestClass]
    public class TransactionsServiceTest
    {
        [TestMethod]
        public void GetAllTRansactionsFromRepositoryTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(tr => tr.TransactionRepository.Get(null, null, ""));

            ITransactionService TransactionService = new TransactionService(mockUnitOfWork.Object);

            IEnumerable<Transaction> returnedCashiers = TransactionService.GetAllTransactions();

            mockUnitOfWork.VerifyAll();
        }



        [TestMethod]
        public void GetTransactionByIdReturnsTransactionWithId()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(tr => tr.TransactionRepository.GetByID(It.IsAny<int>()));

            ITransactionService transactionService = new TransactionService(mockUnitOfWork.Object);

            Transaction returnedTransaction = transactionService.GetTransactionById(1);

            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        public void CreateTransactionTest()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            
            mockUnitOfWork.Setup(tr => tr.TransactionRepository.Insert(It.IsAny<Transaction>()));
           
           
            mockUnitOfWork.Setup(tr => tr.Save());

            ITransactionService transactionService = new TransactionService(mockUnitOfWork.Object);
            
            
            int transaction = transactionService.CreateTransaction(getTransaction());
            mockUnitOfWork.VerifyAll();

        }


        [TestMethod]
        //public void UpdatesExistingUser()
        //{

        //    var mockUnitOfWork = new Mock<IUnitOfWork>();

        //    mockUnitOfWork.Setup(un => un.BillRepository.GetByID(It.IsAny<int>())).Returns(new Bill() { });

        //    mockUnitOfWork.Setup(un => un.BillRepository.Update(It.IsAny<Bill>()));
        //    mockUnitOfWork.Setup(un => un.Save());

        //    IBillService billService = new BillService(mockUnitOfWork.Object);

        //    bool updated = billService.UpdateBill( 0, new Bill() { });

        //    mockUnitOfWork.Verify(un => un.BillRepository.Update(It.IsAny<Bill>()), Times.Exactly(1));
        //    mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));

        //    Assert.IsTrue(updated);

        //}


        private TransactionDTO getTransaction()
        {
            TransactionDTO transaction = new TransactionDTO()
            {
                TransactionDate = "23/12/2016"
            };
            return transaction;
        }

    }
}
