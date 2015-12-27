using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementSystem.Model
{
    public class BaseEntity : IObjectWithState
    {
        public int Id { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}