using API.Database;

namespace API.Services.company.interfaces;

public interface ICompanyRepository
{
    Company[] GetCompaniesByIds(Guid?[] ids);
}