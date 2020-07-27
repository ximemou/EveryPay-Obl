using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class NonExistingPaymentException:Exception
    {
        public NonExistingPaymentException()
        {
        }

        public NonExistingPaymentException(string message) : base(message)
        {
        }

        public NonExistingPaymentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonExistingPaymentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
