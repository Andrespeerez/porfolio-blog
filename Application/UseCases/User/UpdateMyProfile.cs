using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.UseCases;

public class UpdateMyProfile
{
    private readonly IUserRepository _userRepository;

    public UpdateMyProfile(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<UserOutput?> ExecuteAsync(
        int id,
        UpdateMyProfileInput updateUser
    )
    {       
        User? user = await _userRepository.GetByIdAsync(id);
            
        if (user is null || user.IsDeleted())
        {
            return null;
        }

        user.UpdateProfile(
            updateUser.Slug,
            updateUser.FullName,
            updateUser.PhotoUrl,
            updateUser.ExternalWebsiteUrl,
            updateUser.JobTitle,
            updateUser.Bio,
            updateUser.EducationalInstitution,
            updateUser.Topics,
            updateUser.SocialNetworks
        );

        await _userRepository.UpdateAsync(user);

        return UserOutput.FromEntity(user);
    }
}