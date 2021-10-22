using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridgeBLL;
using ShopBridgeBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeWebAPI
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogController : ControllerBase
    {

        IProductCatalogService _prodCatalogSvc;
        IMapper _mapper;
        public ProductCatalogController( IProductCatalogService prodCatalogSvc
                                        ,IMapper mapper)
        {
            _prodCatalogSvc = prodCatalogSvc;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int Id)
        {
            if (Id < 1)
            {
                ModelState.AddModelError(Id.ToString(), "Invalid Product Id");
                return ValidationProblem(ModelState);
            }

            var productBO = await _prodCatalogSvc.GetProduct(Id);

            if (productBO == null)
                return NoContent();

            return Ok(_mapper.Map<ProductBO, ProductModel>(productBO));
        }

        [HttpGet]
        public IEnumerable<ProductModel> GetAllProducts()       
        {
           var prodList = _prodCatalogSvc.GetAllProducts();
           return _mapper.Map<IEnumerable<ProductModel>>(prodList);         
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostProduct(ProductModel product)
        {
            if (product.Id != 0)
            {
                ModelState.AddModelError(product.Id.ToString(), "Invalid Product Details");
                return ValidationProblem(ModelState);
            }

            var productBO = _mapper.Map <ProductModel, ProductBO>(product);
            int prodId = await _prodCatalogSvc.AddProduct(productBO);

            return CreatedAtAction(nameof(GetProduct), new { Id = prodId }, prodId);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduct(ProductModel product)
        {
            if (product.Id < 1)
            {
                ModelState.AddModelError(product.Id.ToString(), "Invalid Product Id");
                return ValidationProblem(ModelState);
            }

            var productBO = _mapper.Map<ProductModel, ProductBO>(product);            
            var result = await _prodCatalogSvc.UpdateProduct(productBO);

            if (result == -1)
                return NotFound();
            else if (result == 0)
                return Ok("Non updated content");
            
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (productId < 1)
            {
                ModelState.AddModelError(productId.ToString(), "Invalid Product Id");
                return ValidationProblem(ModelState);
            }

            var result = await _prodCatalogSvc.DeleteProduct(productId);
            
            if (result != 1)
                return NotFound();

            return Ok();
        }
    }
}
