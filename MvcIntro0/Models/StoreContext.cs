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
            => Database.EnsureCreated();
        /*public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder mdlBldr)
        {
            Role administrator = new Role { RoleId = 1, Title = "admin" };
            Role customer = new Role { RoleId = 2, Title = "customer" };
            Account adminAcnt = new Account { AccountId = 1, Name = "admin", Password = "admin", RoleId = administrator.RoleId };
            Account customerAcnt = new Account { AccountId = 2, Name = "customer", Password = "customer", RoleId = customer.RoleId };
            mdlBldr.Entity<Role>().HasData(new Role[] { administrator, customer });
            mdlBldr.Entity<Account>().HasData(new Account[] { adminAcnt, customerAcnt });
            base.OnModelCreating(mdlBldr);
        }*/
    }
}
