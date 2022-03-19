using System;
using System.Runtime.Serialization;

namespace Discount.API.Application.Exceptions
{
    [Serializable]
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException() { }

        public AlreadyExistsException(Type type) : base($"{type} is missing") { }

        protected AlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public AlreadyExistsException(string message) : base(message) { }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
