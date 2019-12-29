using Recruitment.API.Models;
using System.Collections.Generic;

namespace Recruitment.API.ViewModels
{
    public class CandidateViewModel : Candidate
    {
        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}