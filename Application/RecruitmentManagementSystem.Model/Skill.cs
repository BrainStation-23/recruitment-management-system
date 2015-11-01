﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentManagementSystem.Model
{
    public class Skill : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}