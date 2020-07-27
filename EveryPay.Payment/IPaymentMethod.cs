using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Payment
{
   public interface IPaymentMethod
    {
        bool PayTransaction(float amountGiven,float amountToPay);
        
    }
}
