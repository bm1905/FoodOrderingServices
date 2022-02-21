using System.Globalization;

namespace Catalog.API.Helpers.Exceptions
{
    public class UnauthorizedException : System.Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
