using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class WrongClientIdentification : Exception
    {
        public WrongClientIdentification()
        {
        }

        public WrongClientIdentification(string message) : base(message)
        {
        }

        public WrongClientIdentification(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongClientIdentification(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
