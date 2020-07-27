﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EveryPay.Exceptions
{
    [Serializable]
    public class DateFormatException:Exception
    {
        public DateFormatException()
        {
        }

        public DateFormatException(string message) : base(message)
        {
        }

        public DateFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DateFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
