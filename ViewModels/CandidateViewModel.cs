using Recruitment.API.Models;
using System.Collections.Generic;

namespace Recruitment.API.ViewModels
{
    public class CandidateViewModel : Candidate
    {
        public List<Status> Statuses { get; set; }
    }
}