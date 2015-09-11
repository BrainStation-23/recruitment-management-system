using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementSystem.Model
{
    public class CandidatePlatform
    {
        public int CandidatePlatformId { get; set; }
        [Key]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public string Platform { get; set; }
        public string Duration { get; set; }
        public virtual Candidate Candidate { get; set; }
    }
}
