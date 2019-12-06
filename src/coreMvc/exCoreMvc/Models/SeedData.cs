using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exCoreMvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace exCoreMvc.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            await using (var context=new MovieDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieDbContext>>()))
            {
                if (context.Movies.Count()>5)
                {
                    Log.Information("Seed is not running.");
                    return;
                }

                await context.Movies.AddRangeAsync(
                    new Movie()
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    }, new Movie()
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    });
                await context.SaveChangesAsync();
                Log.Information("Seed is worked.");
            }

        }
    }
}
