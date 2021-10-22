using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridgeBLL
{
    public class ProductBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvialable { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
