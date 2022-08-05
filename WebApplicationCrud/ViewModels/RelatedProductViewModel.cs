using System.Collections.Generic;
using WebApplicationCrud.Models;

namespace WebApplicationCrud.ViewModels
{
    public class RelatedProductViewModel
    {
        public int ProductId { get; set; }
        //List of Badges or Banners or smth whether it is new in sale or Hot or WHatever

        public int ProductInfoId { get; set; }
        public float Price { get; set; }
        public List<Thumbnail> Thumbnails { get; set; }
        public List<string> Colors { get; set; }

    }
}
