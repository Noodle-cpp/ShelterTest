using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    [Serializable]
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException()
        {
        }

        public CompanyNotFoundException(string? message) : base(message)
        {
        }

        public CompanyNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CompanyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
