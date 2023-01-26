using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.ViewModels.AdministrationVms
{
    public class AccountViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Additional Address Info")]
        public string Address2 { get; set; }
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Display(Name = "AdditionalDescription")]
        public string AdditionalDescription { get; set; }
    }
}
