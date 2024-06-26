using API.Database;
using API.Domain.shared;
using API.Domain.tools;
using API.Services.company.interfaces;

namespace API.Services.company;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private ICompanyService _companyServiceImplementation;

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
            Guid id = company.Value.Length > 0 ? Guid.Parse(company.Value) : new Guid();
            Company? existsСompany = _companyRepository.GetAllCompany().FirstOrDefault(x => x.Id == id);

            if (id != existsСompany?.Id && existsСompany?.Name.Trim() == company.Label.Trim())
            {
                response.AddError("Такая компания уже есть");
            }
            else if (existsСompany == null)
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
        Guid id = Guid.Parse(companyId);
        Company company = _companyRepository.GetCompanyById(id);
        company.Isremoved = true;

        _companyRepository.RemoveCompany(company);

        return new Response();
    }

    public Item? GetCompanyByName(string companyName)
    {
        Company? company = _companyRepository.GetCompanyByName(companyName);
        
        return company != null 
            ? new Item(company.Id.ToString(), company.Name)
            : null;
    }
    private void AddCompany(Item company)
    {
        Guid id = String.IsNullOrWhiteSpace(company.Value)
            ? Guid.NewGuid()
            : Guid.Parse(company.Value);
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