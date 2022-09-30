using System.Collections.Generic;

namespace WebApplicationCrud.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Email { get; set; }

        public string PhoneNum { get; set; }
        public List<PaymentInfo> PaymentInfo { get; set; }

    }
}
