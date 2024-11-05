using Api.Attributes;
using Api.Exceptions;
using Api.ViewModels;
using Api.ViewModels.Request;
using Data;
using Data.Models;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.ViewModels.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IApiObjectConverter _apiObjectConverter;

        public CompaniesController(ICompanyService companyService,
                                   IApiObjectConverter apiObjectConverter)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _apiObjectConverter = apiObjectConverter ?? throw new ArgumentNullException(nameof(apiObjectConverter));
        }

        /// <summary>
        /// CRUD по компаниям
        /// Поскольку было указано
        /// "Стиль REST - да, игнорируется, внутри JSON должно быть указание на вид операции"
        /// сделала единый метод, где указывается вид операции
        /// </summary>
        /// <param name="requestViewModel">
        /// Содержит тип операции, id и тело запроса
        /// типы операций: CREATE, UPDATE, READ_LIST, READ, DELETE
        /// </param>
        /// <returns></returns>
        // POST <CompaniesController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RequestViewModel requestViewModel)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity();

            try
            {
                var handler = requestViewModel.Operation.ToUpper() switch
                {
                    "CREATE" => CreateCompanyAsync(requestViewModel.Body),
                    "UPDATE" => UpdateCompanyAsync(requestViewModel.Body, requestViewModel.Id),
                    "READ_LIST" => GetCompanyAsync(),
                    "READ" => GetCompanyAsync(requestViewModel.Id),
                    "DELETE" => DeleteCompanyAsync(requestViewModel.Id),
                    _ => throw new NotImplementedException(),
                };

                var response = await handler.ConfigureAwait(false);

                return Ok(response);
            }
            catch (CompanyNotFoundException)
            {
                if (requestViewModel.Operation.ToUpper() is "CREATE") return BadRequest("Parent company doesnt exist");
                else return NotFound();
            }
            catch (NotImplementedException)
            {
                return BadRequest("Operation doesnt exist");
            }
            catch (RequiredArgumentException ex)
            {
                return BadRequest($"Field {ex.Message} is required");
            }
            catch (InvalidBodyException ex)
            {
                return BadRequest($"Field {ex.Message} in body is invalid");
            }
            catch (HabsburgException ex)
            {
                return BadRequest($"Parent company cant be {ex.Message}");
            }
        }

        private async Task<object> CreateCompanyAsync(object? body)
        {
            if(body is null) throw new RequiredArgumentException(nameof(body));

            CreateCompanyViewModel createCompanyViewModel = JsonConvert.DeserializeObject<CreateCompanyViewModel>(body.ToString()) ?? throw new InvalidBodyException(nameof(createCompanyViewModel));

            if (createCompanyViewModel.Name.Length > 255) throw new InvalidBodyException($"{nameof(createCompanyViewModel.Name)} too many symbols max. is 255");
            if (createCompanyViewModel.Inn.Length > 10) throw new InvalidBodyException($"{nameof(createCompanyViewModel.Inn)} too many symbols max. is 10");
            if (createCompanyViewModel.Phone.Length > 30) throw new InvalidBodyException($"{nameof(createCompanyViewModel.Phone)} too many symbols max. is 30");

            _ = createCompanyViewModel.Name ?? throw new InvalidBodyException(nameof(createCompanyViewModel.Name));
            _ = createCompanyViewModel.Inn ?? throw new InvalidBodyException(nameof(createCompanyViewModel.Inn));
            _ = createCompanyViewModel.Phone ?? throw new InvalidBodyException(nameof(createCompanyViewModel.Phone));

            var newCompany = _apiObjectConverter.CompanyFromCreateCompanyViewModel(createCompanyViewModel);

            var company = await _companyService.CreateCompanyAsync(newCompany).ConfigureAwait(false);

            return _apiObjectConverter.CompanyViewModelFromCompany(company);
        }

        private async Task<object> UpdateCompanyAsync(object? body, Guid? id)
        {
            if(body is null) throw new RequiredArgumentException(nameof(body));

            UpdateCompanyViewModel updateCompanyViewModel = JsonConvert.DeserializeObject<UpdateCompanyViewModel>(body.ToString())?? throw new InvalidBodyException(nameof(updateCompanyViewModel));

            if (updateCompanyViewModel.Name.Length > 255) throw new InvalidBodyException($"{nameof(updateCompanyViewModel.Name)} too many symbols max. is 255");
            if (updateCompanyViewModel.Inn.Length > 10) throw new InvalidBodyException($"{nameof(updateCompanyViewModel.Inn)} too many symbols max. is 10");
            if (updateCompanyViewModel.Phone.Length > 30) throw new InvalidBodyException($"{nameof(updateCompanyViewModel.Phone)} too many symbols max. is 30");

            _ = updateCompanyViewModel.Name ?? throw new InvalidBodyException(nameof(updateCompanyViewModel.Name));
            _ = updateCompanyViewModel.Inn ?? throw new InvalidBodyException(nameof(updateCompanyViewModel.Inn));
            _ = updateCompanyViewModel.Phone ?? throw new InvalidBodyException(nameof(updateCompanyViewModel.Phone));

            if (id is null) throw new RequiredArgumentException(nameof(id));

            var updatedCompany = _apiObjectConverter.CompanyFromUpdateCompanyViewModel(updateCompanyViewModel);

            var company = await _companyService.UpdateCompanyAsync(updatedCompany, id.Value).ConfigureAwait(false);

            return _apiObjectConverter.CompanyViewModelFromCompany(company);
        }

        private async Task<object> GetCompanyAsync()
        {
            var companies = await _companyService.GetCompaniesListAsync().ConfigureAwait(false);

            return _apiObjectConverter.CompanyViewModelFromCompany(companies);
        }

        private async Task<object> GetCompanyAsync(Guid? id)
        {
            if (id is null) throw new RequiredArgumentException(nameof(id));

            var company = await _companyService.GetCompanyByIdAsync(id.Value).ConfigureAwait(false);
            return _apiObjectConverter.CompanyViewModelFromCompany(company);
        }

        private async Task<object?> DeleteCompanyAsync(Guid? id)
        {
            if (id is null) throw new RequiredArgumentException(nameof(id));

            await _companyService.DeleteCompanyByIdAsync(id.Value).ConfigureAwait(false);
            return null;
        }
    }
}
