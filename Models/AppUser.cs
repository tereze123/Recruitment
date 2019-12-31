using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class AppUser : IdentityUser
    {
        [DisplayName("Vārds")]
        public string FirstName { get; set; }

        [DisplayName("Uzvārds")]
        public string LastName { get; set; }
    }
}