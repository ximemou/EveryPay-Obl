using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class TransactionAlreadyPayException:Exception
    {
        public TransactionAlreadyPayException()
        {
        }

        public TransactionAlreadyPayException(string message) : base(message)
        {
        }

        public TransactionAlreadyPayException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TransactionAlreadyPayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
