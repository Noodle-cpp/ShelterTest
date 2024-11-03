using System.Runtime.Serialization;

namespace Api.Exceptions
{
    [Serializable]
    public class RequiredArgumentException : Exception
    {
        public RequiredArgumentException()
        {
        }

        public RequiredArgumentException(string? message) : base(message)
        {
        }

        public RequiredArgumentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RequiredArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
