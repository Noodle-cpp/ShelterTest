using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    /// <summary>
    /// Название эту шутка юмора :)
    /// Ошибка возникает, когда объект пытается стать родителем себе или иметь своего ребенка, как родителя
    /// </summary>
    [Serializable]
    public class HabsburgException : Exception
    {
        public HabsburgException()
        {
        }

        public HabsburgException(string? message) : base(message)
        {
        }

        public HabsburgException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected HabsburgException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
