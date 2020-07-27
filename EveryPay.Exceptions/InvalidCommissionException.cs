using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class InvalidCommissionException : Exception
    {
        public InvalidCommissionException()
        {
        }

        public InvalidCommissionException(string message) : base(message)
        {
        }

        public InvalidCommissionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCommissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
