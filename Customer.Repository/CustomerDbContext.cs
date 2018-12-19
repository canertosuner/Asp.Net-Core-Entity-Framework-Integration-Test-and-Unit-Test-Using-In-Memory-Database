

using Microsoft.EntityFrameworkCore;

namespace Customer.Repository
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
