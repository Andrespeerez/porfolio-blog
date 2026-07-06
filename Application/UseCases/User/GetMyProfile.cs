using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.UseCases;

public class GetMyProfile
{
    private readonly IUserRepository _userRepository;

    public GetMyProfile(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<UserOutput?> ExecuteAsync(
        int currentUserId
    )
    {
        User? myUser = await _userRepository.GetByIdAsync(currentUserId);

        if (myUser is null || myUser.IsDeleted())
        {
            return null;
        }

        var userOutput = UserOutput.FromEntity(myUser);

        return userOutput;
    }
}