using API.Database;
using API.Middleware;
using API.Services.adminPanel;
using API.Services.adminPanel.interfaces;
using API.Services.Auth;
using API.Services.Auth.Interfaces;
using API.Services.company;
using API.Services.company.interfaces;
using API.Services.group;
using API.Services.group.interfaces;
using API.Services.practice;
using API.Services.practice.interfaces;
using API.Services.user;
using API.Services.user.interfaces;
using API.Services.user.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApp.Session";
    
    options.IdleTimeout = TimeSpan.FromSeconds(60 * 60 * 24);
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddSession(opt =>
{
    opt.Cookie.IsEssential = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();

builder.Services.AddTransient<IPracticeService, PracticeService>();
builder.Services.AddTransient<IPracticeRepository, PracticeRepository>();

builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();

builder.Services.AddTransient<IAdminPanelService, AdminPanelService>();

builder.Services.AddTransient<PracticetrackerContext>();

var app = builder.Build();

app.UseSession();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);
app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<CheckSystemUser>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
