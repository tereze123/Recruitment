using System;
using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [DisplayName("Vārds")]
        public string Name { get; set; }

        [DisplayName("Uzvārds")]
        public string Surname { get; set; }

        [DisplayName("Tālruņa numurs")]
        public string PhoneNumber { get; set; }

        [DisplayName("E-pasts")]
        public string Email { get; set; }

        [DisplayName("Vakance")]
        public int VacancyId { get; set; }

        [DisplayName("Vakance")]
        public Vacancy Vacancy { get; set; }

        [DisplayName("Statuss")]
        public int StatusId { get; set; }

        [DisplayName("Statuss")]
        public Status Status { get; set; }

        [DisplayName("Tests")]
        public int? TestId { get; set; }

        [DisplayName("Tests")]
        public Test Test { get; set; }

        [DisplayName("Reģistrācijas datums")]
        public DateTime RegistrationDate { get; set; }
    }
}