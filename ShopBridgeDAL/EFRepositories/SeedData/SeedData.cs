using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopBridgeDAL.EFRepositories
{
        public class SeedData
        {
            public static void InitializeDb(IServiceProvider serviceProvider)
            {

                var shopDbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ShopDbContext>();

                if (shopDbContext.Database.GetPendingMigrations().Count() == 0 &&
                    shopDbContext.Product.Count() == 0)
                {
                  
                    // *************************** PRODUCT *******************************************
                    var prod1 = new Product { Name = "Chess", Description = "Playing Chess Board", Price = 1000, IsAvialable = true, Quantity= 100, IsDeleted = false};
                    var prod2 = new Product { Name = "TV", Description = "Samsung TV", Price = 5000, IsAvialable = true, Quantity = 200, IsDeleted = false };
                    var prod3 = new Product { Name = "Rice Bag", Description = "Basmati Rice Bag 5 kg", IsAvialable = false, Quantity = 400, IsDeleted = false };
                    var prod4 = new Product { Name = "Shirt", Description = "Raymond Shirt", Price = 2000, IsAvialable = false, Quantity = 500, IsDeleted = true };
                    var prod5 = new Product { Name = "Laptop", Description = "Dell Laptop", Price = 4500, IsAvialable = true, Quantity = 600, IsDeleted = false };
                    var prod6 = new Product { Name = "Trousers", Description = "Calvin Clien Trousers", Price = 2200, IsAvialable = true, Quantity = 800, IsDeleted = false };

                    List<Product> prodList = new List<Product> { prod1, prod2, prod3, prod4, prod5, prod6 };

                    shopDbContext.UpdateRange(prodList);
                    shopDbContext.SaveChanges();
                }
            }
        }
    
}
