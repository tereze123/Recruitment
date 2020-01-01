using System.ComponentModel.DataAnnotations;

namespace Recruitment.API.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Lietotājvārds")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parole")]
        public string Password { get; set; }

        [Display(Name = "Atcerēties mani")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}