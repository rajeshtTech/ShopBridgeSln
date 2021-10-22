using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeBLL.Services.EFRepoContracts
{
    public interface IProductCatalogRepository
    {
        Task<ProductBO> GetProduct(int productId);
        IEnumerable<ProductBO> GetAllProducts();
        Task<int> AddProduct(ProductBO product);
        Task<int> UpdateProduct(ProductBO product);
        Task<int> DeleteProduct(int productId);
    }
}
