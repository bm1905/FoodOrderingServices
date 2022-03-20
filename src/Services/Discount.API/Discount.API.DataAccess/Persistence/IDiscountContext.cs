using System.Data;

namespace Discount.API.DataAccess.Persistence
{
    public interface IDiscountContext
    {
        public IDbConnection Connection { get; }
    }
}
