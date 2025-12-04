using Microsoft.EntityFrameworkCore;
using OrderRegistry.Domain.Entities;

namespace OrderRegistry.Infrastructure.Data
{
    public class OrderRegistryDbContext : DbContext
    {
        public OrderRegistryDbContext(DbContextOptions<OrderRegistryDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
