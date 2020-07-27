using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Payment
{
    public class Cash:IPaymentMethod
    {
        public bool PayTransaction(float givenAmount,float amountToPay)
        {
            if (givenAmount >= amountToPay)
            {
                return true;
            }
            return false;
        }
    }
}
