using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels.PanelVMs
{
    public class ProductImagesVm
    {

        public List<ProductImages> ProductImages { get; set; }
    }
    public class ProductImages
    {
        public List<RoomImagesVm> RoomImagesVms { get; set; }
    }
    public class RoomImagesVm
    {
        public int ProductIndex { get; set; }
        public List<IFormFile> RoomImages { get; set; }
        public int? ThumbnailIndex { get; set; }
    }
}
