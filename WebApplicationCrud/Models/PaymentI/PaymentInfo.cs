using System.Collections.Generic;

namespace WebApplicationCrud.Models
{
    public class PaymentInfo
    {
        public int id { get; set; }
        public List<CardInfo> CardInfo { get; set; }
        public List<OtherMethod> otherMethods { get; set; }
    }
}
