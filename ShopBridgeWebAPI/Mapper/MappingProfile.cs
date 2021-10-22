using AutoMapper;
using ShopBridgeBLL;
using ShopBridgeDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeWebAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductBO>().ReverseMap();
            CreateMap<ProductBO, ProductModel>().ReverseMap();
        }
    }
}
