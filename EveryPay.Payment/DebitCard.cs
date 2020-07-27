using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Payment
{
    public class DebitCard:IPaymentMethod
    {
       public bool PayTransaction(float amountGiven, float amountToPay)
        {

            if(amountGiven>=amountToPay)
            {
                return true;

            }
            return false;
        }
    }
}
