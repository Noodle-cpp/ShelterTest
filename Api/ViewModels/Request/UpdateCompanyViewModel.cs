using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Request
{
    public class UpdateCompanyViewModel
    {
        public Guid? ParentCompanyId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Inn { get; set; }

        [Required]
        [MaxLength(12)]
        public string Phone { get; set; }
    }
}
