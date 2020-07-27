
using EveryPay.Data;
using EveryPay.Data.Entities;
using EveryPay.Data.Repository;
using EveryPay.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EveryPay.TokenManagment
{
    public class ValidateRoles
    {
        private readonly IUnitOfWork unitOfWork;

        public ValidateRoles(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ValidateRoles()
        {
            this.unitOfWork = new UnitOfWork();
        }
        
        public bool validate(string aToken,UserRole userRole)
        {
            if(aToken != null)
            {
                Token token = unitOfWork.TokenRepository.Get(t => t.AuthToken == aToken).FirstOrDefault();
                if (token != null)
                {
                    User user = unitOfWork.UserRepository.Get(u => u.UserId == token.UserId).FirstOrDefault();
                    if (userRole.ToString() == user.Role)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
          
        }
    }
}