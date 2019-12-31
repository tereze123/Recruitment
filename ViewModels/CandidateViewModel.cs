using Recruitment.API.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Recruitment.API.ViewModels
{
    public class CandidateViewModel : Candidate
    {
        public List<Skill> Skills { get; set; } = new List<Skill>();

        [DisplayName("Testa rezultāts")]
        public int? TestResult { get; set; }
    }
}