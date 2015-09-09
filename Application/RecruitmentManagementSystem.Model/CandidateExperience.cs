using System;
using System.Collections.Generic;

namespace RecruitmentManagementSystem.Model
{
    public class CandidateExperience
    {
        public int Id { get; set; }
        public List<string> Company { get; set; }
        public DateTime? WorkingFrom { get; set; }
        public DateTime? WorkingTo { get; set; }
    }
}
