using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Models
{
    public class StoreContext : IdentityDbContext<Account>
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        public StoreContext(DbContextOptions<StoreContext> optns) : base(optns)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder mdlBldr)
        {
            mdlBldr.Entity<Role>().HasData(new Role[]
            {
                new Role { RoleId = 1, Title = "admin" },
                new Role { RoleId = 2, Title = "customer" }
            });

            base.OnModelCreating(mdlBldr);
        }
    }
}
