using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class InsufficientPermissionException : Exception
    {
        public InsufficientPermissionException()
        {
        }

        public InsufficientPermissionException(string message) : base(message)
        {
        }

        public InsufficientPermissionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
