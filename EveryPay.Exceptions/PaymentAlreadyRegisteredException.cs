using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
   public class PaymentAlreadyRegisteredException:Exception
    {

        public PaymentAlreadyRegisteredException()
        {
        }

        public PaymentAlreadyRegisteredException(string message) : base(message)
        {
        }

        public PaymentAlreadyRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaymentAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
