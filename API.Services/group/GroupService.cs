using API.Database;
using API.Domain.group;
using API.Domain.shared;
using API.Domain.tools;
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

    public Item[] GetGroupOptions()
    {
        Group[] groups = _groupRepository.GetAllGroup();

        Item[] options = groups
            .Select(group => new Item(group.Id.ToString(), group.Name))
            .ToArray();

        return options;
    }

    public Item[] GetAllGroup()
    {
        Group[] groups = _groupRepository.GetAllGroup();

        Item[] options = groups
            .Select(group => new Item(group.Id.ToString(), group.Name))
            .ToArray();

        return options;
    }

    public Response SaveGroup(Item group)
    {
        Response response = new Response();

        if (String.IsNullOrWhiteSpace(group.Label))
        {
            response.AddError("Заполните название группы");
        }
        else
        {
            if (String.IsNullOrWhiteSpace(group.Value))
            {
                AddGroup(group);
            }
            else
            {
                EditGroup(group);
            }
        }

        return response;
    }

    public Response RemoveGroup(string groupId)
    {
        throw new NotImplementedException();
    }

    private void AddGroup(Item group)
    {
        Guid id = Guid.NewGuid();
        Group newGroup = new Group(id, group.Label);

        _groupRepository.AddGroup(newGroup);
    }
    
    private void EditGroup(Item group)
    {
        Guid id = Guid.Parse(group.Value);
        Group existsGroup = _groupRepository.GetGroupById(id);

        existsGroup.Name = group.Label;

        _groupRepository.EditGroup(existsGroup);
    }
}