using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcIntro0.Models
{
    public class StoreContext : IdentityDbContext<Account>
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }


        public StoreContext(DbContextOptions<StoreContext> optns) : base(optns)
            => Database.EnsureCreated();
    }
}
