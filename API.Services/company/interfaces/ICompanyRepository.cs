using API.Database;
using API.Domain.shared;

namespace API.Services.company.interfaces;

public interface ICompanyRepository
{
    Company[] GetCompaniesByIds(Guid?[] ids);
    Company[] GetAllCompany();
    void AddCompany(Company newCompany);
    Company GetCompanyById(Guid id);
    void EditCompany(Company existsCompany);
    void RemoveCompany(Company company);
    Company? GetCompanyByName(string companyName);
}