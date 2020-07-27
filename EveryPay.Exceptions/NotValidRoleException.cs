using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public  class NotValidRoleException:Exception
    {
        
   
   
        public NotValidRoleException()
        {
        }

        public NotValidRoleException(string message) : base(message)
        {
        }

        public NotValidRoleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotValidRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
