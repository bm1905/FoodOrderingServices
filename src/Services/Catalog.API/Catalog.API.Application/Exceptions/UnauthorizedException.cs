using System;
using System.Runtime.Serialization;

namespace Catalog.API.Application.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(Type type) : base($"{type} is missing") { }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }
}