using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models;
using WebApplicationCrud.ViewModels.HomeVMs;

namespace WebApplicationCrud.Data.AutoMapper
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductInfos.Select(s => s.ImageNames),
                opts => opts.MapFrom(src => src.ProductInfos.Select(s => s.Images.Select(Img => Img.Imagename))))

                .ForMember(dest=>dest.ProductInfos.Select(s=>s.Stock.Select(stock=>stock.Number))
                ,opts=>opts.MapFrom(src=>src.ProductInfos.Select(prod=>prod.ProductInfoStockAndSizes.Select(stock=>stock.Stock))))

                 .ForMember(dest => dest.ProductInfos.Select(s => s.Stock.Select(size => size.SizeName))
                , opts => opts.MapFrom(src => src.ProductInfos.Select(prod => prod.ProductInfoStockAndSizes.Select(size => size.SizeName))))
                 .ReverseMap();
        }
    }
}
