using EveryPay.Data.Entities;
using EveryPay.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Web.Services
{
    public interface  IClientService
    {
        Client GetClientById(int clientId);
        IEnumerable<Client> GetAllClients();
        int CreateClient(ClientDTO aClient);
        bool UpdateClient(int clientId, Client aClient);
        bool DeleteClient(int clientId);
    }
}
