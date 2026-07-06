using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Application.UseCases;

public class UpdateMyProfile
{
   private readonly ISessionManager _sessionManager;
   private readonly IUserRepository _userRepository;

    public UpdateMyProfile(
        ISessionManager sessionManager,
        IUserRepository userRepository
    )
    {
        _sessionManager = sessionManager;
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(
        UpdateOwnProfileInput updateUser
    )
    {
        int? currentUserId = await _sessionManager.GetCurrentUserIdAsync();

        if (currentUserId is null)
        {
            return;
        }

        int id = (int)currentUserId;

        
    }
}