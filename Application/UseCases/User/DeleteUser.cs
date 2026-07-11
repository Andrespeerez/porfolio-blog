using System.Security.Claims;
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
        int userId,
        ClaimsPrincipal currentUser
    )
    {
        if (currentUser is null || !currentUser.IsInRole("admin"))
        {
            return null;
        }

        User? user = await _userRepository.GetByIdAsync(userId);
        if (user is null || user.IsDeleted()) return null;

        var selfIdClaim = currentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(selfIdClaim, out int selfId) && selfId == userId)
        {
            return null;
        }

        user.SoftDelete();
        await _userRepository.UpdateAsync(user);

        return UserOutput.FromEntity(user);
    }
}