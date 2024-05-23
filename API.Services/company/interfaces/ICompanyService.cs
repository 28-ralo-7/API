using API.Domain.shared;
using API.Domain.tools;

namespace API.Services.company.interfaces;

public interface ICompanyService
{
    Item[] GetCompaniesItemsByIds(Guid?[] ids);
    Item[] GetAllCompany();
    Response SaveCompany(Item company);
    Response RemoveCompany(string companyId);
}