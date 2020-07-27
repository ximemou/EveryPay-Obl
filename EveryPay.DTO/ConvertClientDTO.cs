using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.DTO
{
   public  class ConvertClientDTO
    {

        public Client convertDTO(ClientDTO clientDTO)
        {
            Client client = new Client();
            client.Name = clientDTO.Name;
            client.LastName = clientDTO.LastName;
            client.PhoneNumber = clientDTO.PhoneNumber;
            client.Identification = clientDTO.Identification;
            client.Address = clientDTO.Address;
            return client;
        }
    }
}
