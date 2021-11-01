using System;
using System.Linq;
using CoinMarker.Infrastructure.Model;
using CoinMarket.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoinMarket.Data.DataGenerator
{
    public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new UserDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
        {
            if (context.Users.Any())
            {
                return; 
            }

            context.Users.AddRange(
                new UserModel
                {
                    Id = 1,
                    Name = "Murat Bozkurt",
                    Email = "muratbozkurt@paribu.com",
                    Password = "murat1234",
                }
                );

            context.SaveChanges();
        }
    }
}
}