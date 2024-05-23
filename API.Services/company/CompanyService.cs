using API.Database;
using API.Domain.shared;
using API.Domain.tools;
using API.Services.company.interfaces;

namespace API.Services.company;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public Item[] GetCompaniesItemsByIds(Guid?[] ids)
    {
        Company[] companies = _companyRepository.GetCompaniesByIds(ids);
        Item[] companyItems = companies
            .Select(company => new Item(company.Id.ToString(), company.Name))
            .ToArray();

        return companyItems;
    }

    public Item[] GetAllCompany()
    {
        Company[] companies = _companyRepository.GetAllCompany();

        Item[] companyItems = companies
            .Select(company => new Item(company.Id.ToString(), company.Name))
            .ToArray();

        return companyItems;
    }

    public Response SaveCompany(Item company)
    {
        Response response = new Response();

        if (String.IsNullOrWhiteSpace(company.Label))
        {
            response.AddError("Заполните название компании");
        }
        else
        {
            if (String.IsNullOrWhiteSpace(company.Value))
            {
                AddCompany(company);
            }
            else
            {
                EditCompany(company);
            }
        }

        return response;
    }

    public Response RemoveCompany(string companyId)
    {
        throw new NotImplementedException();
    }

    private void AddCompany(Item company)
    {
        Guid id = Guid.NewGuid();
        Company newCompany = new Company(id, company.Label);

        _companyRepository.AddCompany(newCompany);
    }
    
    private void EditCompany(Item company)
    {
        Guid id = Guid.Parse(company.Value);
        Company existsCompany = _companyRepository.GetCompanyById(id);

        existsCompany.Name = company.Label;

        _companyRepository.EditCompany(existsCompany);
    }
}