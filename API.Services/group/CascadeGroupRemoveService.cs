using API.Database;
using API.Domain.tools;
using API.Services.group.interfaces;
using API.Services.practice.interfaces;
using API.Services.user.interfaces;

namespace API.Services.group;

public class CascadeGroupRemoveService : ICascadeGroupRemoveService 
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserService _userService;
    private readonly IPracticeService _practiceService;

    public CascadeGroupRemoveService(IGroupRepository groupRepository, IUserService userService, IPracticeService practiceService)
    {
        _groupRepository = groupRepository;
        _userService = userService;
        _practiceService = practiceService;
    }

    public Response RemoveGroup(string groupId)
    {
        Guid id = Guid.Parse(groupId);
        Group group = _groupRepository.GetGroupById(id);
        group.Isremoved = true;

        _practiceService.RemovePracticeSchedulesByGroupId(group.Id);
        _userService.RemoveUsersByGroup(id);
        _groupRepository.RemoveGroup(group);

        return new Response();
    }
}