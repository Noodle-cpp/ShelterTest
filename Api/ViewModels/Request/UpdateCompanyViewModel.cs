using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels.Request
{
    public class UpdateCompanyViewModel
    {
        public Guid? ParentCompanyId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Inn { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }
    }
}
