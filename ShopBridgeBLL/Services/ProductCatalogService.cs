using ShopBridgeBLL.Services.EFRepoContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeBLL.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        IProductCatalogRepository _prodCatalogRepo;
        public ProductCatalogService(IProductCatalogRepository prodCatalogRepo)
        {
            _prodCatalogRepo = prodCatalogRepo;
        } 
        public async Task<ProductBO> GetProduct(int productId)
        {
            return await _prodCatalogRepo.GetProduct(productId);
        }
        public IEnumerable<ProductBO> GetAllProducts()
        {
           return _prodCatalogRepo.GetAllProducts();
        }
        public async Task<int> AddProduct(ProductBO product)
        {
            return await _prodCatalogRepo.AddProduct(product);
        }
        public async Task<int> UpdateProduct(ProductBO product)
        {
           return await _prodCatalogRepo.UpdateProduct(product);
        }
        public async Task<int> DeleteProduct(int productId)
        {
           return await _prodCatalogRepo.DeleteProduct(productId);
        }
    }
}
