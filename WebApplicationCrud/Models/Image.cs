namespace WebApplicationCrud.Models
{
    public class Image
    {

        public int Id { get; set; }
        public string Imagename { get; set; }
        public Product Product { get; set; }
        public int? ProductId { get; set; }
        
     


    }
}
