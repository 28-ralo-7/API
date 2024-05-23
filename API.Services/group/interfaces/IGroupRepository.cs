using API.Database;

namespace API.Services.group.interfaces;

public interface IGroupRepository
{
    Group GetGroupById(Guid? groupId);
    Group[] GetGroupByUserId(Guid? groupId);
    Group[] GetAllGroup(bool isRemoved = false);
    void EditGroup(Group existsGroup);
    void AddGroup(Group newGroup);
}