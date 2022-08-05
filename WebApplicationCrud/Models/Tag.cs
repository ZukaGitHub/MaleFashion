namespace WebApplicationCrud.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
