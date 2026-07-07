using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class DeleteUser
{
    private readonly IUserRepository _userRepository;

    public DeleteUser(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<UserOutput?> ExecuteAsync(
        int userId
    )
    {
        User? user = await _userRepository.GetByIdAsync(userId);

        if (user is null || user.IsDeleted()) return null;

        user.SoftDelete();
        await _userRepository.UpdateAsync(user);

        return UserOutput.FromEntity(user);
    }
}