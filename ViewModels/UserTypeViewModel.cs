using System.ComponentModel.DataAnnotations;

namespace Recruitment.API.ViewModels
{
    public class UserTypeViewModel : UserInformationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Lietotāja tips")]
        public string Role { get; set; }
    }
}
