namespace Recruitment.API.Models
{
    public class TestResult
    {
        public Candidate Candidate { get; set; }

        public int CandidateId { get; set; }

        public Test Test { get; set; }

        public int TestId { get; set; }

        public int ResultPercentage { get; set; }
    }
}