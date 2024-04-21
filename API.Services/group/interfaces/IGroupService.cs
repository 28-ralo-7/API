using API.Domain.group;

namespace API.Services.group.interfaces;

public interface IGroupService
{
    GroupDomain GetGroupById(Guid? groupId);
}