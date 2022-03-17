using Catalog.API.Application.Services.AccountService;

namespace Catalog.API.Application.Helpers
{
    public static class AccountHelpers
    {
        public static string GetUserName(IAccountService accountService)
        {
            return accountService.Username;
        }
    }
}
