using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.UseCases;

public class GetMyProfile
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionManager _sessionManager;

    public GetMyProfile(
        IUserRepository userRepository,
        ISessionManager sessionManager
    )
    {
        _userRepository = userRepository;
        _sessionManager = sessionManager;
    }

    public async Task<UserOutput?> ExecuteAsync()
    {
        int? myUserId = await _sessionManager.GetCurrentUserIdAsync();

        if (myUserId is null)
        {
            return await Task.FromResult<UserOutput?>(null);
        }

        int id = (int)myUserId;

        User? myUser = await _userRepository.GetByIdAsync(id);

        if (myUser is null)
        {
            return await Task.FromResult<UserOutput?>(null);
        }

        var userOutput = UserOutput.FromEntity(myUser);

        return userOutput;
    }
}