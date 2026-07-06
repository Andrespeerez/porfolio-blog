using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _db.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return _db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<User?> GetBySlugAsync(string slug)
    {
        return _db.Users.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
}