using System.Runtime.Serialization;

namespace Api.Exceptions
{
    [Serializable]
    public class InvalidBodyException : Exception
    {
        public InvalidBodyException()
        {
        }

        public InvalidBodyException(string? message) : base(message)
        {
        }

        public InvalidBodyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidBodyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
