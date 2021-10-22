using ShopBridgeBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopBridgeDAL
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(20,8)")]
        public decimal Price { get; set; }
        public bool IsAvialable { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        public void ToProdcut(ProductBO prod) 
        {
            Name = prod.Name;
            Description = prod.Description;
            Price = prod.Price;
            IsAvialable = prod.IsAvialable;
            Quantity = prod.Quantity;
            IsDeleted = prod.IsDeleted;
        }
    }
}
