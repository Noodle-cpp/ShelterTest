using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICompanyRepository
    {
        public Task CreateCompanyAsync(Company newCompany);
        public Task UpdateCompanyAsync(Company updatedCompany);
        public Task<Company?> GetCompanyByIdAsync(Guid id);
        public Task<IEnumerable<Company>> GetCompaniesByParentIdAsync(Guid parentId);
        public Task<IEnumerable<Company>> GetCompaniesListAsync();
        public Task DeleteCompanyAsync(Company company);
    }
}
