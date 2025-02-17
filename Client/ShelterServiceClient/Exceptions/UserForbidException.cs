﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.Exceptions
{
    [Serializable]
    public class UserForbidException : Exception

    {
        public UserForbidException()
        {
        }

        public UserForbidException(string? message) : base(message)
        {
        }

        public UserForbidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserForbidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
