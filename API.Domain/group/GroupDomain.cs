using API.Database;

namespace API.Domain.group;

public class GroupDomain
{
    public Guid Id { get; set; }
    public String Name { get; set; }

    public GroupDomain(Guid id = new Guid(), string name = "")
    {
        Id = id;
        Name = name;
    }

    public GroupDomain(Group group)
    {
        Id = group.Id;
        Name = group.Name;
    }
}