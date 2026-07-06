using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class GetUserBySlug
{
    private readonly IUserRepository _userRepository;

    public GetUserBySlug(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<UserOutput?> ExecuteAsync(
        string slug
    )
    {
        User? user = await _userRepository.GetBySlugAsync(slug);

        if (user is null || user.IsDeleted())
        {
            return null;
        }

        return UserOutput.FromEntity(user);
    }
}