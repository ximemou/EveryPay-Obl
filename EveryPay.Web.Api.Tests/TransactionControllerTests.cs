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
    public class TransactionControllerTests
    {
        [Fact]
        public void GetAllTransactionsReturnsEverythingInRepository()
        {
            var allTransactions = new[]
            {
                new Transaction()
                {
                    TransactionDate=DateTime.Now,
                   
                    TransactionId=1

                },
                new Transaction()
                {
                     TransactionDate=DateTime.Now,

                      TransactionId=2
                }
            };

            var mockTransactionService = new Mock<ITransactionService>();
            mockTransactionService.Setup(x => x.GetAllTransactions()).Returns(allTransactions);

            var controller = new TransactionsController(mockTransactionService.Object);

            IHttpActionResult actionResult = controller.GetTransactions();

            OkNegotiatedContentResult<IEnumerable<Transaction>> contentResult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<Transaction>>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Same(allTransactions, contentResult.Content);

        }

    }
}
