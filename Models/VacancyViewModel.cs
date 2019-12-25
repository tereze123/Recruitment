using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class VacancyViewModel: Vacancy
    {
        [DisplayName("Pieteikto kandidātu skaits")]
        public int CandidateCount { get; set; }

        /*
        public List<Skill> EducationSkills { get; set; }
        public List<Skill> SkillSkills { get; set; }
        public List<Skill> AbilitySkills { get; set; }
        public List<Skill> ExperienceSkills { get; set; }
        */
    }
}
