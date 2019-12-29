using System.ComponentModel.DataAnnotations;

namespace Recruitment.API.ViewModels
{
    public class UserInformationViewModel
    {

        [Display(Name = "Vārds")]
        public string FirstName { get; set; }

        [Display(Name = "Uzvārds")]
        public string LastName { get; set; }

        [Display(Name = "E-pasts")]
        public string Email { get; set; }
    }
}