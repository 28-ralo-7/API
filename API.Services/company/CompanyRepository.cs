using API.Database;
using API.Services.company.interfaces;

namespace API.Services.company;

public class CompanyRepository : ICompanyRepository
{
    private readonly PracticetrackerContext _context;

    public CompanyRepository(PracticetrackerContext context)
    {
        _context = context;
    }

    public Company[] GetCompaniesByIds(Guid?[] ids)
    {
        return _context.Companies
            .Where(company => ids.Contains(company.Id) && company.Isremoved != true)
            .ToArray();
    }

    public Company[] GetAllCompany()
    {
        return _context.Companies
            .Where(company => company.Isremoved != true)
            .ToArray();
    }

    public Company? GetCompanyById(Guid id)
    {
        return _context.Companies
            .FirstOrDefault(company => company.Id == id && company.Isremoved != true);
    }

    public void AddCompany(Company newCompany)
    {
        _context.Companies.Add(newCompany);
        _context.SaveChanges();;
    }

    public void EditCompany(Company existsCompany)
    {
        _context.Companies.Update(existsCompany);
        _context.SaveChanges();
    }

    public void RemoveCompany(Company company)
    {
        _context.Companies.Update(company);
        _context.SaveChanges();
    }
}