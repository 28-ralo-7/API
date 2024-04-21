using API.Database;
using API.Domain.group;
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
        Group group = _groupRepository.GetGroupById(groupId);
        GroupDomain groupDomain = new GroupDomain(group);

        return groupDomain;
    }
}