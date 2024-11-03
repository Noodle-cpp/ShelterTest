using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyService
    {
        public Task<Company> CreateCompanyAsync(Company newCompany);
        public Task<Company> UpdateCompanyAsync(Company updatedCompany, Guid id);
        public Task<Company> GetCompanyByIdAsync(Guid id);
        public Task<IEnumerable<Company>> GetCompaniesListAsync();
        public Task DeleteCompanyByIdAsync(Guid id);
    }
}
