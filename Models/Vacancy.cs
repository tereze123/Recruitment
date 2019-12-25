using System;
using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class Vacancy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Atvēršanas datums")]
        public DateTime OpeningDate { get; set; }

        [DisplayName("Plānotais aizvēršanas datums")]
        public DateTime ClosingDate { get; set; }
        
    }
}