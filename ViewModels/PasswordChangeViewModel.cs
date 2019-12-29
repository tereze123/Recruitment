using System.ComponentModel.DataAnnotations;

namespace Recruitment.API.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parole")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jaunā parole")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jaunā parole vēlreiz")]
        public string NewPasswordConfirmed { get; set; }
    }
}