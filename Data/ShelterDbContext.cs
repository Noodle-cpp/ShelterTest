using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ShelterDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ShelterDbContext(DbContextOptions<ShelterDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
