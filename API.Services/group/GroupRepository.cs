using API.Database;
using API.Services.group.interfaces;

namespace API.Services.group;

public class GroupRepository : IGroupRepository
{
    private readonly PracticetrackerContext _context;

    public GroupRepository(PracticetrackerContext context)
    {
        _context = context;
    }

    public Group GetGroupById(Guid? groupId)
    {
        return _context.Groups.FirstOrDefault(group => group.Id == groupId && !group.Isremoved);
    }

    public Group[] GetGroupByUserId(Guid? practiceLeadId)
    {
        Practiceschedule[] practices = _context.Practiceschedules
            .Where(practice => practice.Practiceleadid == practiceLeadId  
                               && practice.Isremoved != true)
            .ToArray();
        Guid[] groupIds = practices.Select(practice => practice.Groupid).ToArray();

        List<Group> groups = new List<Group>();

        foreach (var group in _context.Groups)
        {
            if (groupIds.Contains(group.Id))
            {
                groups.Add(group);
            }
        }
        return groups.ToArray();
    }
}