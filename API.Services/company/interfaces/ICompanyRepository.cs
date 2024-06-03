using API.Database;

namespace API.Services.company.interfaces;

public interface ICompanyRepository
{
    Company[] GetCompaniesByIds(Guid?[] ids);
    Company[] GetAllCompany();
    void AddCompany(Company newCompany);
    Company GetCompanyById(Guid id);
    void EditCompany(Company existsCompany);
    void RemoveCompany(Company company);
}