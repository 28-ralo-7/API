using System.ComponentModel.DataAnnotations;
using API.Domain.group;
using API.Domain.tools;
using API.Services.group.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class GroupController : BaseController
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [Authorize(Roles = "2")]
    [HttpGet]
    public Response GetGroupsByPermissions()
    {
        Response response = new Response();
        GroupDomain[] groupDomains = _groupService.GetGroupsByPermission(SystemUser);
        
        if (groupDomains == null || groupDomains.Length == 0)
        {
            response.AddError("Группы не найдены");
        }
        else
        {
            GroupView[] groupViews = groupDomains.Select(group => new GroupView(group)).ToArray();
            response = new Response(groupViews);
        }
        
        return response;
    }
}