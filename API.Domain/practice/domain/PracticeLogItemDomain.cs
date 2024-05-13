using API.Database;
using API.Domain.shared;
using API.Domain.user;

namespace API.Domain.practice.domain;

public class PracticeLogItemDomain
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int? Grade { get; set; }
    public Item? Company { get; set; }
    public string? Contract { get; set; }
    public string? Report { get; set; }

    public PracticeLogItemDomain(string id = "", string name = "")
    {
        Id = id;
        Name = name;
    }

    public PracticeLogItemDomain(Practicelog practicelog, UserDomain user, Item? company)
    {
        Id = practicelog.Id.ToString();
        Name = user.Surname + " " + user.Name + " " + user.Patronomic;
        Grade = practicelog.Grade;
        Company = company;
        Contract = practicelog.Contract;
        Report = practicelog.Report;
    }
}