using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveryPay.Data.Repository;
using Moq;
using System.Collections.Generic;
using EveryPay.Data.Entities;
using System.Data.Entity;
using EveryPay.Data.DataAccess;
using System.Linq;
using EveryPay.Enumerators;

namespace EveryPay.Web.Services.Tests
{
    [TestClass]
    public class SuppliersServiceTest
    {
        [TestMethod]
        public void GetAllSuppliersFromRepositoryTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.SupplierRepository.Get(s=>s.Delete==false, null, "")).Returns(GetSuppliersList());
           
            ISupplierService supplierService = new SupplierService(mockUnitOfWork.Object);

            IEnumerable<Supplier> suppliers = supplierService.GetAllSuppliers();

            mockUnitOfWork.VerifyAll();



             


        }


        [TestMethod]
        public void GetSupplierByIdReturnsSupplierWithId()
        {
          

            var data = GetSuppliersList();
            var set = new Mock<DbSet<Supplier>>().SetupData(data);

            var context = new Mock<EveryPayContext>();
            context.Setup(ctx => ctx.Set<Supplier>()).Returns(set.Object);
            var unitOfWork = new UnitOfWork(context.Object);
            Supplier result = unitOfWork.SupplierRepository.Get(s => s.SupplierId ==1 && s.Delete == false, null, "").FirstOrDefault();

            ISupplierService supplierService = new SupplierService(unitOfWork);
            Supplier aSupplier = supplierService.GetSupplierById(1);

            Assert.AreEqual(result, aSupplier);


          

        }


        [TestMethod]
        public void CreateSupplierTest()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();

           
            mockUnitOfWork.Setup(un => un.SupplierRepository.Insert(It.IsAny<Supplier>()));
            mockUnitOfWork.Setup(un => un.Save());

            ISupplierService supplierService = new SupplierService(mockUnitOfWork.Object);

            int supplier = supplierService.CreateSupplier( getSupplier());

            mockUnitOfWork.VerifyAll();

        }

        [TestMethod]
        public void UpdatesExistingSupplier()
        {



            var data = GetSuppliersList();
            var set = new Mock<DbSet<Supplier>>().SetupData(data);

            var context = new Mock<EveryPayContext>();
            context.Setup(ctx => ctx.Set<Supplier>()).Returns(set.Object);



            var unitOfWork = new UnitOfWork(context.Object);


            ISupplierService supplierService = new SupplierService(unitOfWork);
            Supplier newSupplier = new Supplier();
            newSupplier.Commission = 10;
            newSupplier.Name = "ANTEL2";

            Supplier supplier = unitOfWork.SupplierRepository.GetByID(1);

            bool updated = supplierService.UpdateSupplier(1, newSupplier);



            Assert.IsTrue(updated);






        }


        [TestMethod]
        public void DoesntUpdateNonExistingSupplier()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.SupplierRepository.GetByID(It.IsAny<Supplier>()));
            mockUnitOfWork.Setup(un => un.Save());
            ISupplierService supplierService = new SupplierService(mockUnitOfWork.Object);

            bool update = supplierService.UpdateSupplier(0, new Supplier() { });

            mockUnitOfWork.Verify(un => un.SupplierRepository.Update(It.IsAny<Supplier>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(update);



        }

        [TestMethod]
        public void DeleteSupplier()
        {





            var data = GetSuppliersList();
            var set = new Mock<DbSet<Supplier>>().SetupData(data);

            var context = new Mock<EveryPayContext>();
            context.Setup(ctx => ctx.Set<Supplier>()).Returns(set.Object);
            var unitOfWork = new UnitOfWork(context.Object);

            ISupplierService supplierService = new SupplierService(unitOfWork);

            bool delete = supplierService.DeleteSupplier(1);
            Assert.IsTrue(delete);


        }

        [TestMethod]
        public void DoesntDeleteNonExistingSupplier()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.SupplierRepository.GetByID(It.IsAny<Supplier>()));
            mockUnitOfWork.Setup(un => un.Save());
            ISupplierService supplierService = new SupplierService(mockUnitOfWork.Object);

            bool update = supplierService.DeleteSupplier(0);

            mockUnitOfWork.Verify(un => un.SupplierRepository.Update(It.IsAny<Supplier>()), Times.Never());
            mockUnitOfWork.Verify(un => un.Save(), Times.Never());
            Assert.IsFalse(update);
        }


        private Supplier getSupplier()
        {
            Supplier supplier = new Supplier()
            {
                Name = "ANTEL",
                Commission = 1
              
                
            };
            return supplier;
        }

        private List<Supplier> GetSuppliersList()
        {
            return new List<Supplier>
            {
                new Supplier()
                {
                    Name="ANTEL",
                    Commission=1,
                    Delete=false,
                    SupplierId=1,
                    SupplierFields= getSupplierFields()

                },
                new Supplier()
                {
                    Name="OSE",
                    Commission=2,
                    Delete=false,
                    SupplierId=2,
                    SupplierFields=getSupplierFields()
                }

            };
        }

      

        private IEnumerable<Supplier> getSupplierById()
        {
            return new List<Supplier>
            {
                new Supplier()
                {
                    Name="ANTEL",
                    Commission=1,
                    Delete=false,
                    SupplierFields=getSupplierFields(),
                    SupplierId=1


                }
            };
        }


        private List<SupplierField> getSupplierFields()
        {
            return new List<SupplierField>
            {
                new SupplierField()
                {
                    FieldName="Fecha Vencimiento",
                    SupplierFieldId=1,
                 
                    TypeOfField=SupplierFieldType.Date.ToString()

                },
                new SupplierField()
                {
                    FieldName="Numero socio",
                    SupplierFieldId=2,
                   
                    TypeOfField=SupplierFieldType.Numeric.ToString()
                }

            };
        }
    }
}
