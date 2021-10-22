using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridgeDAL.EFRepositories
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> shopDbContext): base(shopDbContext)
        {           
        }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<Product>()
                        .Property<DateTime>("LastUpdated")
                        .HasDefaultValueSql("GETDATE()");
        }
    }
}
