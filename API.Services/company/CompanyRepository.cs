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
        return _context.Companies.Where(company => ids.Contains(company.Id) && company.Isremoved != true).ToArray();
    }
}