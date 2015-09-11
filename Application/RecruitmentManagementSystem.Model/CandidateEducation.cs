using System.Collections.Generic;

namespace RecruitmentManagementSystem.Model
{
    public enum Degree
    {
        Bsc, Msc
    }
    public class CandidateEducation
    {
        public int CandidateEducationId { get; set; }
        public int CandidateId { get; set; }
        public Degree Degree { get; set; }
        public List<string> Subject { get; set; }
        public List<string> University { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}
