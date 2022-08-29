using System.ComponentModel.DataAnnotations;

 namespace WebApplicationCrud.ViewModels.BlogVMs
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
