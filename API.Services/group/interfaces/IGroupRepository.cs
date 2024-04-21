using API.Database;

namespace API.Services.group.interfaces;

public interface IGroupRepository
{
    Group GetGroupById(Guid? groupId);
}