using System;

namespace Recruitment.API.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime ClosingDate { get; set; }
        
    }
}