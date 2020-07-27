using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveryPay.Data.Entities;
using Moq;
using EveryPay.Data.Repository;
using System.Collections.Generic;
using EveryPay.DTO;
using System.Data.Entity;
using EveryPay.Data.DataAccess;
using EveryPay.Exceptions;
using System.Linq.Expressions;

namespace EveryPay.Web.Services.Tests
{
    [TestClass]
    public class ClientServiceTest
    {
        [TestClass]
        public class UserServiceTest
        {
            [TestMethod]
            public void GetAllClientsFromRepositoryTest()
            {
                var mockUnitOfWork = new Mock<IUnitOfWork>();

                mockUnitOfWork.Setup(un => un.ClientRepository.Get(c => c.Deleted == false, null, "")).Returns(GetClientList());

                IClientService clientService = new ClientService(mockUnitOfWork.Object);

                IEnumerable<Client> returnedClients = clientService.GetAllClients();

                mockUnitOfWork.VerifyAll();
            }


            [TestMethod]
            public void GetClientsByIdReturnsClientWithId()
            {
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(un => un.ClientRepository.GetByID(It.IsAny<int>()));
                IClientService clientService = new ClientService(mockUnitOfWork.Object);
                Client returnedClient = clientService.GetClientById(1);
                mockUnitOfWork.VerifyAll();



            }


            [TestMethod]
            public void CreateClientTest()
            {

                var mockUnitOfWork = new Mock<IUnitOfWork>();


                mockUnitOfWork.Setup(un => un.ClientRepository.Insert(It.IsAny<Client>()));
                mockUnitOfWork.Setup(un => un.Save());

                IClientService clientService = new ClientService(mockUnitOfWork.Object);

                int client = clientService.CreateClient(getClient());

                mockUnitOfWork.VerifyAll();

            }

            [TestMethod]
            public void UpdatesExistingClient()
            {


                //getbyid no lo agarra 

                var data = GetClientList();
                var set = new Mock<DbSet<Client>>().SetupData(data);

                var context = new Mock<EveryPayContext>();
                context.Setup(ctx => ctx.Set<Client>()).Returns(set.Object);

                var unitOfWork = new UnitOfWork(context.Object);


                IClientService clientService = new ClientService(unitOfWork);
                Client newClient = new Client();
                newClient.Name = "Pedro";
                newClient.LastName = "Lopez";
                newClient.Address = "Rivera 2345";
                newClient.Identification = "12345678";
                newClient.PhoneNumber = "23456789";
                newClient.ClientId = 1;

                //no funciona el getbyid aca 
                Client client = unitOfWork.ClientRepository.GetByID(1);

                bool updated = clientService.UpdateClient(1, newClient);



                Assert.IsTrue(updated);


            }

            [TestMethod]
            public void CreateClientThatAlreadyExistsThrowExceptionTest()
            {
                //Arrange
                var data = GetClientList();
                var set = new Mock<DbSet<Client>>().SetupData(data);

                var context = new Mock<EveryPayContext>();
                context.Setup(ctx => ctx.Set<Client>()).Returns(set.Object);



                var unitOfWork = new UnitOfWork(context.Object);
                IClientService clientService = new ClientService(unitOfWork);

                try
                {
                    ClientDTO clientDto = getClient();
                    clientDto.Identification = "12345678";
                    var client =clientService.CreateClient(clientDto);
                    Assert.IsTrue(false);
                }
                catch (NotUniqueException ex)
                {
                    //for debug purposes
                    var errorMessage = ex.Message;
                    Assert.IsTrue(true);
                }
            }




            [TestMethod]
            public void CreateClientWithWrongIdentificationThrowExceptionTest()
            {
                //Arrange
                var data = GetClientList();
                var set = new Mock<DbSet<Client>>().SetupData(data);

                var context = new Mock<EveryPayContext>();
                context.Setup(ctx => ctx.Set<Client>()).Returns(set.Object);



                var unitOfWork = new UnitOfWork(context.Object);
                IClientService clientService = new ClientService(unitOfWork);

                try
                {
                    ClientDTO clientDto = getClient();
                    clientDto.Identification = "12345";
                    var client = clientService.CreateClient(clientDto);
                    Assert.IsTrue(false);
                }
                catch (WrongClientIdentification ex)
                {
                    //for debug purposes
                    var errorMessage = ex.Message;
                    Assert.IsTrue(true);
                }
            }


            [TestMethod]
            public void CreateClientWithoutNameThrowsExceptionTest()
            {
                //Arrange
                var data = GetClientList();
                var set = new Mock<DbSet<Client>>().SetupData(data);

                var context = new Mock<EveryPayContext>();
                context.Setup(ctx => ctx.Set<Client>()).Returns(set.Object);



                var unitOfWork = new UnitOfWork(context.Object);
                IClientService clientService = new ClientService(unitOfWork);

                try
                {
                    ClientDTO clientDto = getClient();
                    clientDto.Name = "";
                    var client = clientService.CreateClient(clientDto);
                    Assert.IsTrue(false);
                }
                catch (WrongDataTypeException ex)
                {
                    //for debug purposes
                    var errorMessage = ex.Message;
                    Assert.IsTrue(true);
                }
            }

            [TestMethod]
            public void GetClientNotFoundTestThrowsException()
            {
                var clientId = 1;
                //Arrange
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                mockUnitOfWork.Setup(un => un.ClientRepository.Get(It.IsAny<Expression<Func<Client, bool>>>(), null, ""));

                IClientService clientService = new ClientService(mockUnitOfWork.Object);
                try
                {
                    var searchedClient = clientService.GetClientById(clientId);
                    Assert.IsNull(searchedClient);

                }
                catch(NotFoundException ex)
                {
                    var errorMessage = ex.Message;
                       Assert.IsTrue(true);
                    mockUnitOfWork.Verify(un => un.ClientRepository.Get(It.IsAny<Expression<Func<Client, bool>>>(), null, ""));
                }
               
            }



            private ClientDTO getClient()
            {
                ClientDTO aClient = new ClientDTO()
                {

                    Name = "Pedro",
                    LastName = "Lopez",
                    Address = "Rivera 2345",
                    Identification = "12345679",
                    PhoneNumber = "25069786",
                    


                };
                return aClient;

            }

            private List<Client> GetClientList()
            {
                return new List<Client>()
                {
                    new Client()
                    {

                    Name = "Pedro",
                    LastName = "Lopez",
                    Address = "Rivera 2345",
                    Identification = "12345678",
                    PhoneNumber = "25069786",
                    ClientId=1,


                     },
                    new Client()
                     {

                    Name = "Juan",
                    LastName = "Martinez",
                    Address = "Rivera 1234",
                    Identification = "34528679",
                    PhoneNumber = "28069786",
                    ClientId=2,


                     }

                };
            }



        }
    }
}
    
