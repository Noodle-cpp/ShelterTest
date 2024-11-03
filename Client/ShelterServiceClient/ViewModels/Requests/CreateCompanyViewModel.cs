using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterServiceClient.ViewModels.Requests
{
    public class CreateCompanyViewModel
    {
        public Guid? ParentCompanyId { get; set; }

        public string Name { get; set; }

        public string Inn { get; set; }

        public string Phone { get; set; }
    }
}
