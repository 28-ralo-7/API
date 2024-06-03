using API.Domain.tools;

namespace API.Services.group.interfaces;

public interface ICascadeGroupRemoveService
{
    Response RemoveGroup(string groupId);
}