namespace WebApplication2.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Recruitment.API.Models;

    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestResult>()
                .HasKey(tr => new { tr.CandidateId, tr.TestId });
        }

        public DbSet<Vacancy> Vacancies { get; set; }

        public DbSet<Candidate> Candidates { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<ObjectSkill> ObjectSkills { get; set; }

        public DbSet<ObjectType> ObjectTypes { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<SkillType> SkillTypes { get; set; }
    }
}