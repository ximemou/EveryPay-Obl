using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveryPay.Data.Repository;
using Moq;
using System.Collections.Generic;
using EveryPay.Data.Entities;


namespace EveryPay.Web.Services.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void GetAllUsersFromRepositoryTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.Get(null, null, ""));

            IUserService UserService = new UserService(mockUnitOfWork.Object);

           IEnumerable<User> returnedCashiers = UserService.GetAllUsers();

            mockUnitOfWork.VerifyAll();
        }


        [TestMethod]
        public void GetUsersByIdReturnsUserWithId()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(un => un.UserRepository.GetByID(It.IsAny<int>()));
            IUserService UserService = new UserService(mockUnitOfWork.Object);
            User returnedUser = UserService.GetUserById(1);
            mockUnitOfWork.VerifyAll();


        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
            .Setup(un => un.UserRepository.GetByID(It.IsAny<int>()))
            .Returns(new User() { });
            mockUnitOfWork.Setup(un => un.UserRepository.Delete(It.IsAny<int>()));
            mockUnitOfWork.Setup(un => un.Save());

            IUserService userService = new UserService(mockUnitOfWork.Object);
            bool deleted = userService.DeleteUser(It.IsAny<int>());
            mockUnitOfWork.Verify(un => un.UserRepository.Delete(It.IsAny<int>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));
            Assert.IsTrue(deleted);
        }

        [TestMethod]
        public void CreateUserTest()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();


            mockUnitOfWork.Setup(un => un.UserRepository.Insert(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());



            IUserService UserService = new UserService(mockUnitOfWork.Object);
            

            int User = UserService.CreateUser(getUser());
            mockUnitOfWork.VerifyAll();




          
          

        }


        [TestMethod]
        public void UpdatesExistingUser()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(un => un.UserRepository.GetByID(It.IsAny<int>())).Returns(new User() { });

            mockUnitOfWork.Setup(un => un.UserRepository.Update(It.IsAny<User>()));
            mockUnitOfWork.Setup(un => un.Save());

            IUserService UserService = new UserService(mockUnitOfWork.Object);

            bool updated = UserService.UpdateUser(0, getUser());

            mockUnitOfWork.Verify(un => un.UserRepository.Update(It.IsAny<User>()), Times.Exactly(1));
            mockUnitOfWork.Verify(un => un.Save(), Times.Exactly(1));

            Assert.IsTrue(updated);





       
        }



        private User getUser()
        {
            User aUser = new User()
            {

                Name = "Pedro",
                LastName="Lopez",
                UserName="Pedrito",
                Password = "123456",
               Role="Administrator"

            };
            return aUser;

        }







    }
}
