using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.Exceptions
{
    [Serializable]
    public class UserUnauthorizedException : Exception
    {
        public UserUnauthorizedException()
        {
        }

        public UserUnauthorizedException(string? message) : base(message)
        {
        }

        public UserUnauthorizedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
