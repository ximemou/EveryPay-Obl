using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Data.Entities
{
    public  class User
    {

        public User()
        {
            this.Name = "";
            this.LastName = "";
            this.UserName = "";
            this.Password = "";
            this.Role = "";

        
        }

        public User(string name,string lastName, string userName, string password,string role)
        {
            
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Role = role;
        }
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }  
        public string LastName { get; set; }   
        public string UserName { get; set; }
        public string Password { get; set; } 
        public string Role { get; set; }
    }
}
