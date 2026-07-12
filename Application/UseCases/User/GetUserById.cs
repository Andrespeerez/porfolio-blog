using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class GetUserById
{
    private readonly IUserRepository _userRepository;

    public GetUserById(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<UserOutput?> ExecuteAsync(
        int id
    )
    {
        User? user = await _userRepository.GetByIdAsync(id);

        if (user is null || user.IsDeleted())
        {
            return null;
        }

        return UserOutput.FromEntity(user);
    }
}