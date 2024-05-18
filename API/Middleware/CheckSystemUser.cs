using API.Domain.user;
using static API.Tools.SessionExtensions;

namespace API.Middleware;

public class CheckSystemUser
{
    private readonly RequestDelegate next;

    public CheckSystemUser(RequestDelegate next)
    {
        this.next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        Boolean isAuthRequest = context.Request.Path.Value != null &&
                                (context.Request.Path.Value.Contains("Auth/Auth")
                                 || context.Request.Path.Value.Contains("Auth/CheckAuthAndPermission")
                                 || context.Request.Path.Value.Contains("Auth/LogOn")
                                 || context.Request.Path.Value.Contains("/swagger"));
        
        if (isAuthRequest)
        {
            await next.Invoke(context);
        }
        else
        {
            var user = context.Session.Get<UserDomain>("user");

            if (user?.Id != null)
            {
                await next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }
}