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
        return _context.Groups.FirstOrDefault(group => group.Id == groupId);
    }

    public Group[] GetGroupByUserId(Guid? practiceLeadId)
    {
        Practiceschedule[] practices = _context.Practiceschedules
            .Where(practice => practice.Practiceleadid == practiceLeadId)
            .ToArray();
        Guid[] groupIds = practices.Select(practice => practice.Groupid).ToArray();
        
        return _context.Groups.Where(group => groupIds.Contains(group.Id)).ToArray();
    }
}