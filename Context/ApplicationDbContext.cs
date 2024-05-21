using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TP_Portal.Model.MyApplicationUser;

namespace TP_Portal.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMedia> OrderMedias { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<AssignOrder> AssignOrders { get; set; }
        public DbSet<Pricing> Pricing { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Add your model configurations here
        }
    }
}

