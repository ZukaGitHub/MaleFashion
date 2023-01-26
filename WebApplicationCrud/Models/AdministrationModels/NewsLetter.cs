using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCrud.Models.AdministrationModels
{
    public class NewsLetter
    {
        public int Id { get; set; }
        public DateTime DateReceived { get; set; }
     
        public string Email { get; set; }
    }
}
