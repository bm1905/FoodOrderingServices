using System.Security.Principal;

namespace Catalog.API.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        public AccountService(IPrincipal claims)
        {
            Username = claims?.Identity?.Name ?? "system";
            IsAuthenticated = claims?.Identity?.IsAuthenticated ?? false;
        }
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
