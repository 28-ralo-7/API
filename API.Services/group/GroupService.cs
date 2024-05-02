using API.Database;
using API.Domain.group;
using API.Domain.user;
using API.Services.group.interfaces;

namespace API.Services.group;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public GroupDomain GetGroupById(Guid? groupId)
    {
        if (groupId == null) return null;

        Group group = _groupRepository.GetGroupById(groupId);
        GroupDomain groupDomain = new GroupDomain(group);

        return groupDomain;

    }

    public GroupDomain[] GetGroupsByPermission(UserDomain systemUser)
    {
        Group[] groups = _groupRepository.GetGroupByUserId(systemUser.Id);

        GroupDomain[] groupDomains = groups.Select(group => new GroupDomain(group)).ToArray();
        
        return groupDomains;
    }
}