using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exCoreMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace exCoreMvc.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
    }
}
