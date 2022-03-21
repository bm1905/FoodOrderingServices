using Discount.API.Application.Services.AccountService;

namespace Discount.API.Application.Helpers
{
    public static class AccountHelper
    {
        public static string GetUserName(IAccountService accountService)
        {
            return accountService.Username;
        }
    }
}
