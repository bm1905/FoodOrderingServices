using System.Globalization;

namespace Catalog.API.Helpers.Exceptions
{
    public class InternalServerErrorException : System.Exception
    {
        public InternalServerErrorException() : base() { }

        public InternalServerErrorException(string message) : base(message) { }

        public InternalServerErrorException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
