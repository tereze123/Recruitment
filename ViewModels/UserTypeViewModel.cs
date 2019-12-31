using Recruitment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recruitment.API.ViewModels
{
    public class UserTypeViewModel : UserInformationViewModel
    {
        public Role Role { get; set; }
    }
}
