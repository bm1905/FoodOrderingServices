using System.Security.Principal;

namespace Discount.API.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        public AccountService(IPrincipal claims)
        {
            Username = claims?.Identity?.Name ?? "system";
            IsAuthenticated = claims?.Identity?.IsAuthenticated ?? false;
        }

        public bool IsAuthenticated { get; set; }

        public string Username { get; set; }
    }
}
