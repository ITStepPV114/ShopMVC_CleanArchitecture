using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string? Surname { get; set; }
        public string? Firstname { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Order>? Orders {get; set;}
    }
}
