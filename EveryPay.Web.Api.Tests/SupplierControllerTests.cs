using EveryPay.Data.Entities;
using EveryPay.Web.Api.Controllers;
using EveryPay.Web.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;
using System.Web.Http.Hosting;
using System.Net.Http;

namespace EveryPay.Web.Api.Tests
{
    public class SupplierControllerTests
    {

        [Fact]
        public void GetAllSuppliersReturnsEverythingInRepository()
        {
            var allSuppliers = new[]
            {
                new Supplier()
                {
                    

                    Name="ANTEL",
                    Commission= 1,
                    SupplierId=1
                    

                },
                new Supplier()
                {
                     Name="OSE",
                     Commission= 2,
                     SupplierId=2
                }
            };

            var mockSupplierService = new Mock<ISupplierService>();
            mockSupplierService.Setup(x => x.GetAllSuppliers()).Returns(allSuppliers);

            var controller = new SuppliersController(mockSupplierService.Object);

            IHttpActionResult actionResult = controller.GetSuppliers();

            OkNegotiatedContentResult<IEnumerable<Supplier>> contentResult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<Supplier>>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Same(allSuppliers, contentResult.Content);

        }

        [Fact]
        public void UpdateSupplierReturnsNoContent()
        {
            var mockSupplierService = new Mock<ISupplierService>();

            mockSupplierService.Setup(x => x.UpdateSupplier(It.IsAny<int>(), It.IsAny<Supplier>())).Returns(true);
            var controller = new SuppliersController(mockSupplierService.Object);
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

            IHttpActionResult actionResult = controller.PutSupplier(0, new Supplier() { SupplierId = 0 });
            ResponseMessageResult contentResult = Assert.IsType<ResponseMessageResult>(actionResult);

         
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.Response.StatusCode, HttpStatusCode.NoContent);
           

        }


    }
}
