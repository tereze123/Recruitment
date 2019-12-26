namespace Recruitment.API.Models
{
    public class Skill
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public SkillType SkillType { get; set; }

        public int SkillTypeId { get; set; }
    }
}