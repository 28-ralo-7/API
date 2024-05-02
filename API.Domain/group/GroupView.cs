namespace API.Domain.group;

public class GroupView
{
    public String Id { get; set; }
    public String Name { get; set; }

    public GroupView(GroupDomain groupDomain)
    {
        Id = groupDomain.Id.ToString();
        Name = groupDomain.Name;
    }
}