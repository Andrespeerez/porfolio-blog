using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases;
using Domain.Entities;
using Infrastructure.Auth;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Api.Auth; 
using Infrastructure.Persistence.Seed;
using UI; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);
 
// 1) DB
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));
 
// 2) Auth (cookie scheme + identity hasher)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/login";
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
 
// 3) Puertos -> Adaptadores
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, IdentityPasswordHasher>();
builder.Services.AddScoped<ISessionManager, CookieSessionManager>();
 
// 4) Caso de uso
builder.Services.AddScoped<AuthenticateUser>();
builder.Services.AddScoped<RegisterUser>();
builder.Services.AddScoped<LogoutUser>();

// 5) Seeder
builder.Services.AddTransient<AdminUserSeeder>();
 
// 6) Blazor
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});
 
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<AdminUserSeeder>();
    await seeder.SeedAsync();
}
 
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapLogin();
app.MapLogout();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
