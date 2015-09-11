using System;
using System.Collections.Generic;

namespace RecruitmentManagementSystem.Model
{
    public class CandidateExperience
    {
        public int CandidateExperienceId { get; set; }
        public int CandidateId { get; set; }
        public List<string> Company { get; set; }
        public DateTime? WorkingFrom { get; set; }
        public DateTime? WorkingTo { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}
