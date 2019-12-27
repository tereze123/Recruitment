using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class VacancyViewModel: Vacancy
    {
        [DisplayName("Pieteikto kandidātu skaits")]
        public int CandidateCount { get; set; }

        [DisplayName("Kompetences tips")]
        public int SkillTypeId { get; set; }

        [DisplayName("Kompetences vērtība")]
        public string SkillValue { get; set; }
    }
}
