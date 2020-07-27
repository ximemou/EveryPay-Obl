using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;
using EveryPay.Exceptions;
using EveryPay.Enumerators;

namespace EveryPay.Web.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork aUnitOfWork)
        {
            this.unitOfWork = aUnitOfWork;
        }

        public UserService()
        {
            unitOfWork = new UnitOfWork();
        }

        public int CreateUser( User userToCreate)
        {
         
            if (ValidateNewUser(userToCreate) && !ExistsUserWithSameUserName(userToCreate.UserName))
            {
                if (ValidateRole(userToCreate.Role))
                {
                    unitOfWork.UserRepository.Insert(userToCreate);
                    unitOfWork.Save();
                    return userToCreate.UserId;
                }
                else
                {
                    throw new NotValidRoleException("El rol debe ser Administrator o Cashier");
                }
            }
            else
            {
                throw new WrongDataTypeException("Datos mal ingresados");
            }
        }

        public bool DeleteUser(int UserId)
        {
            if (ExistsUser(UserId))
            {
                unitOfWork.UserRepository.Delete(UserId);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public User GetUserById(int UserId)
        {
            return unitOfWork.UserRepository.GetByID(UserId);

        }

        public IEnumerable<User> GetAllUsers()
        {
            return unitOfWork.UserRepository.Get();
        }

        public bool UpdateUser(int UserId, User UserToUpdate)
        {
           
            if (ExistsUser(UserId))
            {
                if (!ExistsUserWithSameUserNameWhenUpdate(UserId, UserToUpdate.UserName) && ValidateRole(UserToUpdate.Role)){
                    User newUser = unitOfWork.UserRepository.GetByID(UserId);
                    newUser.Name = UserToUpdate.Name;
                    newUser.LastName = UserToUpdate.LastName;
                    newUser.UserName = UserToUpdate.UserName;
                    newUser.Password = UserToUpdate.Password;
                    newUser.Role = UserToUpdate.Role;
                    unitOfWork.UserRepository.Update(newUser);
                    unitOfWork.Save();
                    return true;
                }
                else if(ExistsUserWithSameUserNameWhenUpdate(UserId,UserToUpdate.UserName))
                {
                    throw new NotUniqueException("Nombre de usuario ya existe");
                }
                else
                {
                    throw new NotValidRoleException("El rol debe ser Cashier o Administrator");
                }
               
            }

            return false;
        }

        private bool ExistsUser(int UserId)
        {
            return unitOfWork.UserRepository.GetByID(UserId) != null;
        }

        private bool ExistsUserWithSameUserName(string userName)
        {

            IEnumerable<User> users = unitOfWork.UserRepository.Get(u => u.UserName == userName);
            return users.Count() > 0;
        }

        private bool ValidateNewUser(User User)
        {
            bool correct;


            correct = ValidateCompleteName(User.Name, User.LastName, User.Role)
                   && ValidatePassword(User.Password);

            return correct;
        }

        private bool ExistsUserWithSameUserNameWhenUpdate(int userId, string userName)
        {

            IEnumerable<User> users = unitOfWork.UserRepository.Get(u => u.UserName == userName && u.UserId != userId);
            return users.Count() > 0;
        }

        private bool ValidateRole(string role)
        {
            if (Enum.IsDefined(typeof(UserRole), role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //POS: retorna true si no es vacio
        private bool ValidateCompleteName(string name, string lastName, string userName)
        {
            return (name.Length != 0 && lastName.Length != 0 && userName.Length != 0);
        }
       
        //POS: retorna true si no es vacio
        private bool ValidatePassword(string password)
        {
            return password.Length != 0;
        }

        private UserRole GetUserRole(string role)
        {

            if (Enum.IsDefined(typeof(UserRole), role))
            {

                return (UserRole)Enum.Parse(typeof(UserRole), role, true);
            }
            else
            {
                throw new NotValidRoleException("Debe ser un rol valido");
            }
        }
    }
}
