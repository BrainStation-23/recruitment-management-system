namespace RecruitmentManagementSystem.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string ContactNo { get; set; }
        //public string Password { get; set; }
        public string ResumeAttachmentPath { get; set; }
        public string PhotoAttachmentPath { get; set; }
        public virtual CandidateEducation Degree { get; set; }
        public virtual CandidateEducation Subject { get; set; }
        public virtual CandidateEducation University { get; set; }
        public virtual CandidateExperience Company { get; set; }
        public virtual CandidateExperience WorkingFrom { get; set; }
        public virtual CandidateExperience WorkingTo { get; set; }
        public virtual CandidatePlatform Platform  { get; set; }
        public virtual CandidatePlatform Duration { get; set; }
    }
}
