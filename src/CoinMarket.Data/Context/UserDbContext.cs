using CoinMarker.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace CoinMarket.Data.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}