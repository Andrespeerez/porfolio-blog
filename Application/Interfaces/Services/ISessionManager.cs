using Domain.Entities;

namespace Application.Interfaces.Services;

public interface ISessionManager
{
    Task SignInAsync(User user);
    Task SignOutAsync();
    Task<int?> GetCurrentUserIdAsync();
}