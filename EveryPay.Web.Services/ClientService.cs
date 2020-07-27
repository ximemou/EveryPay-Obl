using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;
using EveryPay.Exceptions;
using EveryPay.DTO;

namespace EveryPay.Web.Services
{
    public class ClientService : IClientService
    {

        private readonly IUnitOfWork unitOfWork;



        public ClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ClientService()
        {
            this.unitOfWork = new UnitOfWork();
        }
        public int CreateClient(ClientDTO aClient)
        {
            ConvertClientDTO converted = new ConvertClientDTO();

            Client client = converted.convertDTO(aClient);
            if (ValidateUniqueClient(client))
            {
                if (ValidateIdentification(client.Identification))
                {
                    if (ValidateNewClient(client))
                    {

                        unitOfWork.ClientRepository.Insert(client);
                        unitOfWork.Save();
                        return client.ClientId;


                    }
                    else
                    {
                        throw new WrongDataTypeException("Datos mal ingresados, todos los campos son obligatorios");
                    }
                }
                else
                {
                    throw new WrongClientIdentification("La cedula debe tener 8 digitos");
                }
            }
            else
            {
                throw new NotUniqueException("El cliente ya existe");
            }
        }

        private bool ValidateNewClient(Client aClient)
        {

            return ValidateName(aClient.Name, aClient.LastName) && ValidatePhoneNumber(aClient.PhoneNumber)
                   && ValidateAddress(aClient.Address) ;
        }

        private bool ValidateName(string name, string lastName)
        {
            return name.Length != 0 && lastName.Length != 0;
        }

        private bool ValidatePhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length != 0;
        }

        private bool ValidateAddress(string address)
        {
            return address.Length != 0;
        }

        private bool ValidateIdentification(string identification)
        {
            return identification.Length==8;
        }

        public bool DeleteClient(int clientId)
        {
            if (ExistsClient(clientId))
            {
                Client client = unitOfWork.ClientRepository.GetByID(clientId);
                client.Deleted = true;
                unitOfWork.ClientRepository.Update(client);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return unitOfWork.ClientRepository.Get(c=>c.Deleted==false);
        }

        public Client GetClientById(int clientId)
        {
            if (ExistsClient(clientId))
            {
                return unitOfWork.ClientRepository.GetByID(clientId);
            }
            else
            {
                throw new NotFoundException("No se encontro el cliente buscado");
            }
        }

        public bool UpdateClient(int clientId, Client aClient)
        {
            if (ExistsClient(clientId))
            {
                Client client = unitOfWork.ClientRepository.GetByID(clientId);
                client.Address = aClient.Address;

                client.PhoneNumber = aClient.PhoneNumber;

                unitOfWork.ClientRepository.Update(client);
                unitOfWork.Save();

                return true;

            }
            return false;
        }


        private bool ExistsClient(int clientId)
        {
            return unitOfWork.ClientRepository.Get(c=>c.ClientId==clientId && c.Deleted==false).FirstOrDefault() != null;
        }

        private bool ValidateUniqueClient(Client client)
        {
            return unitOfWork.ClientRepository.Get(c => c.Identification == client.Identification).Count()== 0;
        }


    }
}
