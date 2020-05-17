using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cw10.Exceptions
{
    public class StudentAlreadyExistsException : Exception
    {
        public StudentAlreadyExistsException()
        {
        }

        public StudentAlreadyExistsException(string message) : base(message)
        {
        }

        public StudentAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StudentAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
