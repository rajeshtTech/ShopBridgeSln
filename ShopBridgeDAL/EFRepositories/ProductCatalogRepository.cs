using ShopBridgeBLL;
using ShopBridgeBLL.Services.EFRepoContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShopBridgeDAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;

namespace ShopBridgeDAL.EFRepositories
{
    public class ProductCatalogRepository : IProductCatalogRepository
    {
        ShopDbContext _shopDbContext;
        IMapper _mapper;
        public ProductCatalogRepository(ShopDbContext shopDbContext, IMapper mapper)
        {
            _shopDbContext = shopDbContext;
            _mapper = mapper;
        }

        public async Task<ProductBO> GetProduct(int productId)
        {
             var product = await _shopDbContext.Product.AsNoTracking().FirstOrDefaultAsync(prod => prod.Id == productId);
             return _mapper.Map<Product, ProductBO>(product);
        }

        public IEnumerable<ProductBO> GetAllProducts()
        {
            var prodList = _shopDbContext.Product.AsNoTracking();
            return _mapper.ProjectTo<ProductBO>(prodList).ToList();
        }
        public async Task<int> AddProduct(ProductBO productBO)
        {
            var product = _mapper.Map<ProductBO, Product>(productBO);
            await _shopDbContext.Product.AddAsync(product);
            await _shopDbContext.SaveChangesAsync();
            return product.Id;
        }
        public async Task<int> UpdateProduct(ProductBO productBO)
        {
            var existingProd = await _shopDbContext.Product
                                    .IgnoreQueryFilters()
                                    .FirstOrDefaultAsync(prod => prod.Id == productBO.Id);

            if (existingProd == null)
                return -1;

            existingProd.ToProdcut(productBO); 
            return await _shopDbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteProduct(int productId)
        {
            _shopDbContext.Product.Remove(new Product { Id = productId });
            return await _shopDbContext.SaveChangesAsync();
        }     
    }
}
