using System.Collections.Generic;
using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class VacancyViewModel: Vacancy
    {
        [DisplayName("Pieteikto kandidātu skaits")]
        public int CandidateCount { get; set; }

        public List<Skill> Skills { get; set; } = new List<Skill>();
    }
}