using Api.ViewModels.Request;
using Data.Models;
using Web.ViewModels.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.ViewModels
{
    /// <summary>
    /// Решила не использовать AutoMapper
    /// </summary>
    public interface IApiObjectConverter 
    {
        CompanyViewModel CompanyViewModelFromCompany(Company company);
        Company CompanyFromCreateCompanyViewModel(CreateCompanyViewModel createCompanyViewModel);
        Company CompanyFromUpdateCompanyViewModel(UpdateCompanyViewModel createCompanyViewModel);
        IEnumerable<CompanyViewModel> CompanyViewModelFromCompany(IEnumerable<Company> companies);
    }

    public class ApiObjectConverter : IApiObjectConverter
    {
        public Company CompanyFromCreateCompanyViewModel(CreateCompanyViewModel createCompanyViewModel)
        {
            return new Company()
            {
                Inn = createCompanyViewModel.Inn,
                Name = createCompanyViewModel.Name,
                ParentCompanyId = createCompanyViewModel.ParentCompanyId,
                Phone = createCompanyViewModel.Phone,
            };
        }

        public Company CompanyFromUpdateCompanyViewModel(UpdateCompanyViewModel updateCompanyViewModel)
        {
            return new Company()
            {
                Inn = updateCompanyViewModel.Inn,
                Name = updateCompanyViewModel.Name,
                ParentCompanyId = updateCompanyViewModel.ParentCompanyId,
                Phone = updateCompanyViewModel.Phone,
            };
        }

        public CompanyViewModel CompanyViewModelFromCompany(Company company)
        {
            return new CompanyViewModel()
            {
                Id = company.Id,
                Inn = company.Inn,
                Name = company.Name,
                ParentCompanyId = company.ParentCompanyId,
                Phone = company.Phone,
            };
        }

        public IEnumerable<CompanyViewModel> CompanyViewModelFromCompany(IEnumerable<Company> companies)
        {
            foreach (var company in companies)
                yield return CompanyViewModelFromCompany(company);
        }
    }
}
