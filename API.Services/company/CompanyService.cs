using API.Database;
using API.Domain.shared;
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
}