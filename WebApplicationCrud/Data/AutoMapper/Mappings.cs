using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.HomeVMs;

namespace WebApplicationCrud.Data.AutoMapper
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest=>dest.Tags,opt=>opt.MapFrom(src=>src.Tags.Select(s=>s.TagName).ToList()))
                .ReverseMap();
            CreateMap<ProductInfo, ProductInfoViewModel>()
                .ForMember(dest => dest.ImageNames, opt => 
                opt.MapFrom(src => src.Images.Select(s => s.Imagename).ToList())).ReverseMap();            
            CreateMap<StockVm, ProductInfoStockAndSize>()
                .ForMember(dest=>dest.Stock,opt=>opt.MapFrom(src=>src.Number))
                .ReverseMap();
          
        }
    }
}
