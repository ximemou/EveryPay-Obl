using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get;set;}
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int TotalPoints { get; set; }

        public bool Deleted { get; set; }

        public Client()
        {
            Deleted = false;
            TotalPoints = 0;
        }
            

    }
}
