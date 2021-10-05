using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialsApi
{
    public class FactorialsContext : DbContext
    {
        public FactorialsContext(DbContextOptions<FactorialsContext> options) : base(options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Factorial> Factorials { get; set; }
    }
}
