using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ShelterDbContext _context;

        public CompanyRepository(ShelterDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateCompanyAsync(Company newCompany)
        {
            await _context.Companies.AddAsync(newCompany).ConfigureAwait(false);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompanyAsync(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesByParentIdAsync(Guid parentId)
        {
            return await _context.Companies.Where(x => x.ParentCompanyId == parentId).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> GetCompaniesListAsync()
        {
            return await _context.Companies.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid id)
        {
            return await _context.Companies.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task UpdateCompanyAsync(Company updatedCompany)
        {
            _context.Companies.Update(updatedCompany);
            await _context.SaveChangesAsync();
        }
    }
}
