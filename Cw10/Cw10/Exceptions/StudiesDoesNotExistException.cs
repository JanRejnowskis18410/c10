using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cw10.Exceptions
{
    public class StudiesDoesNotExistException : Exception
    {
        public StudiesDoesNotExistException()
        {
        }

        public StudiesDoesNotExistException(string message) : base(message)
        {
        }

        public StudiesDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StudiesDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
