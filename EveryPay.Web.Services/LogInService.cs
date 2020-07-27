using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using EveryPay.Data.Repository;
using EveryPay.Data.Entities;
using EveryPay.Exceptions;
using EveryPay.Desktop.LogInterface;
using EveryPay.LogManager;

namespace EveryPay.Web.Services
{
    public class LogInService : ILogInService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogController logController;

        public LogInService()
        {
            unitOfWork = new UnitOfWork();
            logController = new LogController();
        }

        public LogInService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Token GenerateToken(string userName, string password)
        {           
            validateUserNameAndPassword(userName, password);

            User user = unitOfWork.UserRepository.Get(u => u.UserName == userName && u.Password == password).FirstOrDefault();
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiresOn = DateTime.Now.AddHours(1);
            var tokendomain = new Token
            {
                UserId = user.UserId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiresOn,
                User = user
            };

            unitOfWork.TokenRepository.Insert(tokendomain);

            logController.saveInLog("Ingreso|" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|" + userName);

            unitOfWork.Save();

            return tokendomain;
        }

        private void validateUserNameAndPassword(string userName, string password)
        {
            User user = unitOfWork.UserRepository.Get(u => u.UserName == userName && u.Password == password).FirstOrDefault();
            if(user == null)
            {
                throw new NotFoundException("Nombre de usuario o clave incorrecto");
            }
        }

        public bool Kill(string tokenId)
        {
            unitOfWork.TokenRepository.Delete(tokenId);
            unitOfWork.Save();
            var isNotDeleted = unitOfWork.TokenRepository.Get(x => x.AuthToken == tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        public bool ValidateToken(string tokenId)
        {
            Token token = unitOfWork.TokenRepository.Get(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now).FirstOrDefault();
            if (token != null )
            {
                token.ExpiresOn = token.ExpiresOn.AddSeconds(600);
                unitOfWork.TokenRepository.Update(token);
                unitOfWork.Save();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }


        public string GetTokenUser(string userName, string password)
        {
           User user = unitOfWork.UserRepository.Get(u=>u.UserName==userName && u.Password==password).FirstOrDefault();

            if (user != null)
            {
                Token token = unitOfWork.TokenRepository.Get(t => t.UserId == user.UserId).FirstOrDefault();
                return token.AuthToken;
            }
            return null;

        }
    }
}
