using System.ComponentModel.DataAnnotations;

namespace Recruitment.API.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "Lietotājvārds")]
        public string Username { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "Vārds")]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "Uzvārds")]
        public string LastName { get; set; }

        [Required, MaxLength(256)]
        [Display(Name = "E-pasts")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [Display(Name = "Parole")]
        public string Password { get; set; }


        [DataType(DataType.Password), Compare(nameof(Password))]
        [Display(Name = "Parole vēlreiz")]
        public string ConfirmPassword { get; set; }
    }
}
