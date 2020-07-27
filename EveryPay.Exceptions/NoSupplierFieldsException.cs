using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class NoSupplierFieldsException:Exception
    {
        public NoSupplierFieldsException()
        {
        }

        public NoSupplierFieldsException(string message) : base(message)
        {
        }

        public NoSupplierFieldsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSupplierFieldsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
