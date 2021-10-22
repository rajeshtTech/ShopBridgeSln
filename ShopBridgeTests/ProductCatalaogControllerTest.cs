using ShopBridgeWebAPI;
using System;
using Xunit;
using Moq;
using ShopBridgeBLL.Services;
using AutoMapper;
using System.Collections.Generic;
using ShopBridgeBLL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopBridgeTests
{
    public class ProductCatalaogControllerTest
    {
        Mock<IProductCatalogService> prodCatSvcMock = new Mock<IProductCatalogService>();
        Mock<IMapper> mapperMock = new Mock<IMapper>();
        ProductCatalogController prodCatContr;

        [Fact]
        public void GetAllProducts_ShouldBeNonNull()
        {
            prodCatContr = new ProductCatalogController(prodCatSvcMock.Object, mapperMock.Object);
            prodCatSvcMock.Setup(x => x.GetAllProducts()).Returns(GetDummyProductBOList()).Verifiable();
            mapperMock.Setup(x => x.Map<IEnumerable<ProductModel>>(It.IsAny<IEnumerable<ProductBO>>())).Returns(GetDummyProductModelList());

            var prodListActual =  prodCatContr.GetAllProducts();

            prodCatSvcMock.Verify(x => x.GetAllProducts(), Times.Once);
            Assert.NotNull(prodListActual);
        }

        [Fact]
        public void GetAllProducts_ShouldBeNull()
        {
            prodCatContr = new ProductCatalogController(prodCatSvcMock.Object, mapperMock.Object);

            prodCatSvcMock.Setup(x => x.GetAllProducts()).Returns<IEnumerable<ProductBO>>(null).Verifiable();
            mapperMock.Setup(x => x.Map<IEnumerable<ProductModel>>(It.IsAny<IEnumerable<ProductBO>>())).Returns<IEnumerable<ProductModel>>(null);

            var prodListActual = prodCatContr.GetAllProducts();

            prodCatSvcMock.Verify(x => x.GetAllProducts(), Times.Once);
            Assert.Null(prodListActual);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteProduct_ShouldDelete(int prodId)
        {
            prodCatContr = new ProductCatalogController(prodCatSvcMock.Object, mapperMock.Object);
            prodCatSvcMock.Setup(x => x.DeleteProduct(prodId));

            var data = await prodCatContr.DeleteProduct(prodId);

            Assert.IsType<OkResult>(data);
        }



        #region PRIVATE METHODS
        private IEnumerable<ProductBO> GetDummyProductBOList()
        {
            return new List<ProductBO> { new ProductBO
                                            { 
                                             Id = 2, Name = "Jeans", Description = "Peter England Jeans",
                                             IsAvialable = true, IsDeleted = false 
                                            },
                                            new ProductBO
                                            {
                                             Id = 3, Name = "Jeans", Description = "Peter England Jeans",
                                             IsAvialable = true, IsDeleted = false
                                            },
                                            new ProductBO
                                            {
                                             Id = 4, Name = "Jeans", Description = "Peter England Jeans",
                                             IsAvialable = true, IsDeleted = false
                                            }
                                          };
        }

        private IEnumerable<ProductModel> GetDummyProductModelList()
        {
            return new List<ProductModel> { new ProductModel
                                            {
                                             Id = 2, Name = "Jeans", Description = "Peter England Jeans",
                                             IsAvialable = true, IsDeleted = false
                                            },
                                            new ProductModel
                                            {
                                             Id = 3, Name = "Jeans", Description = "Peter England Jeans",
                                             IsAvialable = true, IsDeleted = false
                                            },
                                            new ProductModel
                                            {
                                             Id = 4, Name = "Jeans", Description = "Peter England Jeans",
                                             IsAvialable = true, IsDeleted = false
                                            }
                                          };
        }
        #endregion
    }
}
