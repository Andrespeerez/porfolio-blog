using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Application.UseCases;

public class UpdateUserByAdmin
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserByAdmin(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher
    )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserOutput?> ExecuteAsync(
        UpdateUserByAdminInput userInput
    )
    {
        var user = await _userRepository.GetByIdAsync(userInput.Id);
        if (user is null)
        {
            return null;
        }

        user.ChangeEmail(userInput.Email);
        user.UpdateProfile(
            userInput.Slug,
            userInput.FullName,
            userInput.PhotoUrl,
            userInput.ExternalWebsiteUrl,
            userInput.JobTitle,
            userInput.Bio,
            userInput.EducationalInstitution,
            userInput.Topics,
            userInput.SocialNetworks
        );

        if (!string.IsNullOrWhiteSpace(userInput.Password))
        {
            user.ChangePassword(userInput.Password, _passwordHasher);
        }

        await _userRepository.UpdateAsync(user);

        return UserOutput.FromEntity(user);
    }
}