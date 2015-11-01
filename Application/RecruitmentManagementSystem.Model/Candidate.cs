using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementSystem.Model
{
    public class Candidate : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string LastName { get; set; }

        [EmailAddress]
        [Index("EmailIndex", IsUnique = true)]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(3000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Others { get; set; }

        [Url]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Website { get; set; }

        public virtual ICollection<Education> Educations { get; set; }

        public virtual ICollection<Experience> Experiences { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}