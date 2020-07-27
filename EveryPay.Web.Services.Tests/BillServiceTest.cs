using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveryPay.Data.Repository;
using Moq;
using System.Collections.Generic;
using EveryPay.Data.Entities;

namespace EveryPay.Web.Services.Tests
{
    [TestClass]
    public class BillServiceTest
    {
        [TestMethod]
        public void GetAllBillsFromRepositoryTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.BillRepository.Get(null, null, ""));

            IBillService billService = new BillService(mockUnitOfWork.Object);

            IEnumerable<Bill> returnedSuppliers = billService.GetAllBills();

            mockUnitOfWork.VerifyAll();


        }


        [TestMethod]
        public void GetBillByIdReturnsBillWithId()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.BillRepository.GetByID(It.IsAny<int>()));

            IBillService billService = new BillService(mockUnitOfWork.Object);

            Bill returnedBill = billService.GetBillById(1);

            mockUnitOfWork.VerifyAll();


        }


        [TestMethod]
        public void CreateBillTest()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();


            mockUnitOfWork.Setup(un => un.BillRepository.Insert(It.IsAny<Bill>()));
            mockUnitOfWork.Setup(un => un.Save());

            IBillService billService = new BillService(mockUnitOfWork.Object);

            int bill = billService.CreateBill(new Bill() { });

            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        public void UpdatesExistingBill()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.BillRepository.GetByID(It.IsAny<int>())).Returns(new Bill() { });

            mockUnitOfWork.Setup(un => un.BillRepository.Update(It.IsAny<Bill>()));
            mockUnitOfWork.Setup(un => un.Save());

            IBillService billServices = new BillService(mockUnitOfWork.Object);

            bool updated = billServices.UpdateBill(0, new Bill() { });

            mockUnitOfWork.Verify(un => un.BillRepository.Update(It.IsAny<Bill>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));

            Assert.IsTrue(updated);

        }







    }
}
