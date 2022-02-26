﻿using System.Globalization;

namespace Common.Exceptions
{
    public class AlreadyExistsException : System.Exception
    {
        public AlreadyExistsException() : base() { }

        public AlreadyExistsException(string message) : base(message) { }

        public AlreadyExistsException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
