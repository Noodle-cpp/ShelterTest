using Data.Interfaces;
using Data.Models;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        public async Task<Company> CreateCompanyAsync(Company newCompany)
        {
            if (newCompany.ParentCompanyId is not null)
                _ = await _companyRepository.GetCompanyByIdAsync(newCompany.ParentCompanyId.Value).ConfigureAwait(false) ?? throw new CompanyNotFoundException();

            newCompany.Id = Guid.NewGuid();

            await _companyRepository.CreateCompanyAsync(newCompany).ConfigureAwait(false);

            return await _companyRepository.GetCompanyByIdAsync(newCompany.Id).ConfigureAwait(false) ?? throw new Exception();
        }

        public async Task DeleteCompanyByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id).ConfigureAwait(false) ?? throw new CompanyNotFoundException();
            await _companyRepository.DeleteCompanyAsync(company).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Company>> GetCompaniesListAsync()
        {
            return await _companyRepository.GetCompaniesListAsync().ConfigureAwait(false);
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            return await _companyRepository.GetCompanyByIdAsync(id).ConfigureAwait(false) ?? throw new CompanyNotFoundException();
        }

        public async Task<Company> UpdateCompanyAsync(Company updatedCompany, Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id).ConfigureAwait(false) ?? throw new CompanyNotFoundException();

            if (company.Id == updatedCompany.ParentCompanyId) throw new HabsburgException(updatedCompany.ParentCompanyId.ToString());

            //Вообще по-хорошему нужно смотреть вообще все дерево потомков, но упростила эту логику до первых его потомков 
            var childrenCompanies = await _companyRepository.GetCompaniesByParentIdAsync(id).ConfigureAwait(false);
            if(childrenCompanies.Any(x => x.Id == updatedCompany.ParentCompanyId)) throw new HabsburgException(updatedCompany.ParentCompanyId.ToString());

            company.Phone = updatedCompany.Phone;
            company.Name = updatedCompany.Name;
            company.ParentCompanyId = updatedCompany.ParentCompanyId;
            company.Inn = updatedCompany.Inn;

            await _companyRepository.UpdateCompanyAsync(company).ConfigureAwait(false);

            return await _companyRepository.GetCompanyByIdAsync(id).ConfigureAwait(false);
        }
    }
}
