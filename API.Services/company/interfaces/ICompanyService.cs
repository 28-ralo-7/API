using API.Domain.shared;

namespace API.Services.company.interfaces;

public interface ICompanyService
{
    Item[] GetCompaniesItemsByIds(Guid?[] ids);
}