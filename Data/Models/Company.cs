using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public Company? ParentCompany { get; set; }
        public Guid? ParentCompanyId { get; set; }
        public string Name { get; set; }
        public string Inn { get; set; }
        public string Phone { get; set; }
    }
}
