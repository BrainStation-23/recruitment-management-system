using System.Collections.Generic;

namespace RecruitmentManagementSystem.Model
{
    public enum Degree
    {
        Bsc, Msc
    }
    public class CandidateEducation
    {
        public int Id { get; set; }
        public Degree Degree { get; set; }
        public List<string> Subject { get; set; }
        public List<string> University { get; set; }
    }
}
