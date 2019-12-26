using System;

namespace Recruitment.API.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int VacancyId { get; set; }

        public Vacancy Vacancy { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}