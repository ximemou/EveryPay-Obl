using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Web.Services
{
    public interface IUserService
    {
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        int CreateUser(User UserToCreate);
        bool UpdateUser(int UserId, User UserToUpdate);
        bool DeleteUser(int UserId);
    }
}
