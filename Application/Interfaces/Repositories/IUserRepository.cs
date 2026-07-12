using Application.DTOs.Common;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<PagedResult<User>> GetUsersAsync(ListUsersQuery options);

    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetBySlugAsync(string slug);

    Task AddAsync(User user);
    Task UpdateAsync(User user);
}
