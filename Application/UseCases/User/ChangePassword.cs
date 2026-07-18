using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.UseCases;

public class ChangePassword
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePassword(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher
    )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserOutput?> ExecuteAsync(
        int userId,
        ChangePasswordInput changePasswordInput
    )
    {
        User? user = await _userRepository.GetByIdAsync(userId);

        if( changePasswordInput.NewPassword == changePasswordInput.RepeatNewPassword &&
            user is not null &&
            _passwordHasher.Verify(user.PasswordHash, changePasswordInput.OldPassword))
        {
            user.ChangePassword(changePasswordInput.NewPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);

            return UserOutput.FromEntity(user);
        }

        return null;
    }
}