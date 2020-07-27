using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Exceptions;
using EveryPay.Data.Repository;
using EveryPay.Data.Entities;
using EveryPay.Enumerators;

namespace EveryPay.Desktop.LoginManager
{
    public class LoginValidator
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        private readonly IUnitOfWork unitOfWork;

        public LoginValidator(string userName, string password)
        {
            UserName = userName;
            Password = password;
            unitOfWork = new UnitOfWork();
        }

        public void validateLogin()
        {
            if (!autenticateUser())
            {
                throw new NotFoundException("El nombre de usuario o la contraseña son erroneos");
            }
        }

        private bool autenticateUser()
        {
            if(unitOfWork.UserRepository.Get(c => c.UserName == UserName && c.Password == Password).Count() == 1)
            {
                User userToCheckRole = unitOfWork.UserRepository.Get(c => c.UserName == UserName && c.Password == Password).FirstOrDefault();
                if(hasSufficientPermissions(userToCheckRole))
                {
                    return true;
                }
                else
                {
                    throw new NotValidRoleException("El usuario no tiene los permisos necesarios para acceder al sistema");
                }
            }
            return false;
        }

        private bool hasSufficientPermissions(User user)
        {
            return user.Role == UserRole.Administrator.ToString();
            
        }

        
    }
}
