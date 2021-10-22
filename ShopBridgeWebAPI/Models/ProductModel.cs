using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeWebAPI
{
    public class ProductModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength =2, ErrorMessage ="Min length must be 2 and Max length must be 20")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvialable { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
