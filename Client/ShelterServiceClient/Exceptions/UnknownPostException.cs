using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.Exceptions
{
    [Serializable]
    public class UnknownPostException : Exception
    {
        public UnknownPostException()
        {
        }

        public UnknownPostException(string? message) : base(message)
        {
        }

        public UnknownPostException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnknownPostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
