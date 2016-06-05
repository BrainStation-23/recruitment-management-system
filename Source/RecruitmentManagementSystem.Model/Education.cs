using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementSystem.Model
{
    public class Education : BaseEntity
    {
        [Required]
        public string Degree { get; set; }

        [Required]
        public string FieldOfStudy { get; set; }

        public double Grade { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Activities { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime? EndDate { get; set; }

        public bool Present { get; set; }

        public int InstitutionId { get; set; }

        public Institution Institution { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
