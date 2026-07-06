using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Application.DTOs.User;

namespace Application.UseCases;

public class CreateUserByAdmin
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserByAdmin(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher
    )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserOutput?> ExecuteAsync(
        CreateUserByAdminInput newUser
    )
    {
        var existing = await _userRepository.GetByEmailAsync(newUser.Email);
        if (existing is not null)
        {
            return null;
        }

        var user =  User.Create(
            email: newUser.Email,
            rawPassword: newUser.Password,
            hasher: _passwordHasher,
            isAdmin: newUser.IsAdmin ?? false
        );

        user.UpdateProfile(
            newUser.Slug,
            newUser.FullName,
            newUser.PhotoUrl,
            newUser.ExternalWebsiteUrl,
            newUser.JobTitle,
            newUser.Bio,
            newUser.EducationalInstitution,
            newUser.Topics,
            newUser.SocialNetworks
        );

        await _userRepository.AddAsync(user);

        return UserOutput.FromEntity(user);
    }
}