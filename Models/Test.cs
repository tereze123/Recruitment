using System.ComponentModel;

namespace Recruitment.API.Models
{
    public class Test
    {
        public int Id { get; set; }

        [DisplayName("Testa nosaukums")]
        public string Name { get; set; }
    }
}