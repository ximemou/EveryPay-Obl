
using EveryPay.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Web.Services
{
    public interface ILogInService
    {
        Token GenerateToken(string userName, string password);

        bool ValidateToken(string tokenId);

        bool Kill(string tokenId);

        string GetTokenUser(string userName, string password);
    }
}
