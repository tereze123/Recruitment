namespace Recruitment.API.Models
{
    public class ObjectSkill
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public ObjectType ObjectType { get; set; }

        public int ObjectTypeId { get; set; }

        public Skill Skill { get; set; }

        public int SkillId { get; set; }
    }
}