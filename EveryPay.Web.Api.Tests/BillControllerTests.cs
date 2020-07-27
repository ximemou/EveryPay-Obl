using EveryPay.Data.Entities;
using EveryPay.Web.Api.Controllers;
using EveryPay.Web.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace EveryPay.Web.Api.Tests
{
   public  class BillControllerTests
    {

        [Fact]
        public void GetAllBillsReturnsEverythingInRepository()
        {

            var allBills = new[]
            {
                new Bill()
                {

                   
                   Amount=1000,
                   BillId=1,
                   Supplier= new Supplier()
                   {
                       Name="ANTEL",
                       Commission= 2,
                       SupplierId=1


                   },

                },
                new Bill()
                {


                     Amount=1000,
                     BillId=1,
                     Supplier= new Supplier()
                     {
                       Name="ANTEL",
                       Commission= 2,
                       SupplierId=2


                     },
                 }

            };

            var mockBillService = new Mock<IBillService>();
            mockBillService.Setup(x => x.GetAllBills()).Returns(allBills);

            var controller = new BillsController(mockBillService.Object);

            IHttpActionResult actionResult = controller.GetBills();

            OkNegotiatedContentResult<IEnumerable<Bill>> contentResult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<Bill>>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Same(allBills, contentResult.Content);

        }

     

    }
}
