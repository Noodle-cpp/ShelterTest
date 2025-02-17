﻿using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Request
{
    public class CreateCompanyViewModel
    {
        public Guid? ParentCompanyId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Inn { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }
    }
}
