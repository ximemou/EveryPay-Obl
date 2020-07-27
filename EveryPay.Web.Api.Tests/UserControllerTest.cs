using EveryPay.Data.Entities;
using EveryPay.Enumerators;
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

namespace EveryPay.Web.Api.Tests
{
    public class UserControllerTest
    {

        [Fact]
        public void GetAllUsersReturnsEverythingInRepository()
        {
            var allUsers = new[]
            {
                new User()
                {
                    
                    Name="Juan",
                    LastName="Gonzalez",
                    UserName="juancito",
                    Password="1234yo",
                    Role=UserRole.Administrator.ToString(),
                    UserId=1

                },
                new User()
                {
                    Name="Pedro",
                    LastName="Suarez",
                    UserName="pedrito",
                    Password="123so",
                    Role=UserRole.Cashier.ToString(),
                    UserId=2
                }
            };

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers()).Returns(allUsers);

            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.GetUsers();

            OkNegotiatedContentResult<IEnumerable<User>> contentResult = Assert.IsType<OkNegotiatedContentResult<IEnumerable<User>>>(actionResult);
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.Same(allUsers, contentResult.Content);

        }


        [Fact]
        public void UpdateUserReturnsNoContent()
        {
            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(x => x.UpdateUser(It.IsAny<int>(), It.IsAny<User>())).Returns(true);
            var controller = new UsersController(mockUserService.Object);

            IHttpActionResult actionResult = controller.PutUser(0, new User() { UserId = 0 });

            StatusCodeResult contentResult = Assert.IsType<StatusCodeResult>(actionResult);
            Assert.NotNull(contentResult);
            Assert.Equal(contentResult.StatusCode, HttpStatusCode.NoContent);

        }



    }
}
