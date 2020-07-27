using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class NoSpecificValuesInBillException : Exception
    {
        public NoSpecificValuesInBillException()
        {
        }

        public NoSpecificValuesInBillException(string message) : base(message)
        {
        }

        public NoSpecificValuesInBillException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSpecificValuesInBillException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
