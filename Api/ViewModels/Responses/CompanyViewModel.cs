using Data.Models;

namespace Web.ViewModels.Responses
{
    public class CompanyViewModel
    {
        public Guid Id { get; set; }
        public Guid? ParentCompanyId { get; set; }
        public string Name { get; set; }
        public string Inn { get; set; }
        public string Phone { get; set; }
    }
}
