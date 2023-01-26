using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.Identity;
using WebApplicationCrud.ViewModels;
using WebApplicationCrud.ViewModels.AdministrationVms;
using WebApplicationCrud.ViewModels.CheckOutVMs;
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
            CreateMap<DeliveryInfo, DeliveryInfoViewModel>().ReverseMap();
            CreateMap<ApplicationUser, AccountViewModel>()
                .ForMember(vm => vm.AdditionalDescription, user => user.MapFrom(delivery => delivery.DeliveryInfo.AdditionalDescription))
                 .ForMember(vm => vm.Address, user => user.MapFrom(delivery => delivery.DeliveryInfo.Address))
                  .ForMember(vm => vm.Address2, user => user.MapFrom(delivery => delivery.DeliveryInfo.Address2))
                   .ForMember(vm => vm.Country, user => user.MapFrom(delivery => delivery.DeliveryInfo.Country))
                    .ForMember(vm => vm.City, user => user.MapFrom(delivery => delivery.DeliveryInfo.City))
                     .ForMember(vm => vm.Street, user => user.MapFrom(delivery => delivery.DeliveryInfo.Street))
                     .ReverseMap();




        }


       
    }
}
