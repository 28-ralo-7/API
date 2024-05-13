using API.Database;
using API.Domain.group;
using API.Domain.shared;
using API.Domain.user;

namespace API.Domain.practice.domain;

public class PracticeLogDomain
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public GroupDomain Group { get; set; } = null!;
    public PracticeLogItemDomain[] LogItems { get; set; } = null!;

    public PracticeLogDomain()
    {
    }

    public PracticeLogDomain(Practice practice, Practiceschedule practiceschedule,
        GroupDomain group, Practicelog[] practicelogItems,
        UserDomain[] students, Item[] companies)
    {
        Id = practiceschedule.Id.ToString();
        Name = practice.Name;
        Group = group;
        LogItems = practicelogItems
            .Select(item => 
                new PracticeLogItemDomain(
                    item,
                    students.FirstOrDefault(student => student.Id == item.Userid),
                    companies.FirstOrDefault(student => student.Value == item.Companyid.ToString())
                )
            )
            .ToArray();
    }
}