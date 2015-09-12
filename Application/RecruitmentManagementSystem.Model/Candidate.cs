using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public File Avatar { get; set; }

        [StringLength(3000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Others { get; set; }

        [Url]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Website { get; set; }

        public virtual IList<Education> Educations { get; set; }
        public virtual IList<Experience> Experiences { get; set; }
        public virtual IList<Skill> Skills { get; set; }
        public virtual IList<Project> Projects { get; set; }
        public virtual IList<File> Resumes { get; set; }
    }

    public class Education
    {
        public int Id { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string FieldOfStudy { get; set; }

        public double Grade { get; set; }

        [StringLength(1500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Activites { get; set; }

        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        public int FirstYear { get; set; }
        public int LastYear { get; set; }

        [Required]
        public int InstitutionId { get; set; }

        public virtual Institution Institution { get; set; }
    }

    public class Experience
    {
        public int Id { get; set; }

        [Required]
        public string Organization { get; set; }

        public string JobTitle { get; set; }

        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public bool StillWorking { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Title { get; set; }

        [Url]
        public string Url { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}