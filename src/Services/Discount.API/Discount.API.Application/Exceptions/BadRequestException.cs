﻿using System;
using System.Runtime.Serialization;

namespace Discount.API.Application.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException() { }

        public BadRequestException(Type type) : base($"{type} is missing") { }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
