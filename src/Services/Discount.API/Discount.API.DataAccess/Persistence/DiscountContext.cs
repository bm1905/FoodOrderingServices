using System;
using System.Data;
using Discount.API.DataAccess.Persistence.Configurations;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.API.DataAccess.Persistence
{
    public class DiscountContext : IDiscountContext, IDisposable
    {
        public DiscountContext(IOptions<NpgsqlSettings> configuration)
        {
            Connection = new NpgsqlConnection(configuration.Value.ConnectionString);
        }

        public IDbConnection Connection { get; }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
