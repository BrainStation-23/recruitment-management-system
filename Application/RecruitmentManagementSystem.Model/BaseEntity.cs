using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentManagementSystem.Model
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedAt { get; set; }
    }
}