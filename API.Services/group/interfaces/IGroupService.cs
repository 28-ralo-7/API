using API.Domain.group;
using API.Domain.shared;
using API.Domain.user;

namespace API.Services.group.interfaces;

public interface IGroupService
{
    GroupDomain GetGroupById(Guid? groupId);
    GroupDomain[] GetGroupsByPermission(UserDomain systemUser);
    Item[] GetGroupOptions();
}