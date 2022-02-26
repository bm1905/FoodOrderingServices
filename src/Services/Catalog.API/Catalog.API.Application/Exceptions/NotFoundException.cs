using System;
using System.Runtime.Serialization;

namespace Catalog.API.Application.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(Type type) : base($"{type} is missing") { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}