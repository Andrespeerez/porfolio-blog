using Application.Interfaces.Services;

namespace Application.UseCases;

public class LogoutUser
{
    private readonly ISessionManager _sessionManager;

    public LogoutUser(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }

    public async Task ExecuteAsync()
    {
        await _sessionManager.SignOutAsync();
    }
}