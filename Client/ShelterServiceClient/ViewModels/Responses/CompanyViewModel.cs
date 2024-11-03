using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.ViewModels.Responses
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
