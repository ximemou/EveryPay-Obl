using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class NotEnoughDataException : Exception
    {
        public NotEnoughDataException()
        {
        }

        public NotEnoughDataException(string message) : base(message)
        {
        }

        public NotEnoughDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
