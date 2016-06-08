using System;

namespace RecruitmentManagementSystem.Core.Models.Shared
{
    public class BaseModel
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}