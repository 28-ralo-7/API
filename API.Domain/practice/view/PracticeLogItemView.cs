using API.Domain.practice.domain;
using API.Domain.shared;

namespace API.Domain.practice.view;

public class PracticeLogItemView
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int? Grade { get; set; }
    public Item? Company { get; set; }
    public string? Contract { get; set; }
    public string? Report { get; set; }

    public PracticeLogItemView(PracticeLogItemDomain domain)
    {
        Id = domain.Id;
        Name = domain.Name;
        Grade = domain.Grade;
        Company = domain.Company;
        Contract = domain.Contract;
        Report = domain.Report;
    }
}