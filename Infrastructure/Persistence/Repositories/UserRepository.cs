using Application.DTOs.Common;
using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<User>> GetUsersAsync(ListUsersQuery options)
    {
        var query = _db.Users.AsNoTracking();

        if (options.Filters is not null && options.Filters.Count > 0)
        {
            var pred = PredicateBuilder.New<User>();

            foreach(var filter in options.Filters)
            {
                var value = filter.Value.Trim().ToLower();
                var key = filter.Key.ToLower();

                switch(key)
                {
                    case "email":
                        pred = pred.Or(x => x.Email.ToLower().Contains(value));
                        break;
                    case "slug":
                        pred = pred.Or(x => x.Slug.ToLower().Contains(value));
                        break;
                    case "name":
                        pred = pred.Or(x => x.FullName != null && x.FullName.ToLower().Contains(value));
                        break;
                    case "id" when int.TryParse(value, out var id):
                        pred = pred.Or(x => x.Id == id);
                        break;
                }
            }

            var finalPredicate = pred.And(x => x.DeletedAt == null);
            query = query.Where(finalPredicate);
        }

        int totalCount = await query.CountAsync();

        int skipCuantity = (options.Page - 1) * options.PageSize;

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skipCuantity)
            .Take(options.PageSize)
            .ToListAsync();
        
        return new PagedResult<User>(
            Items: items,
            Page: options.Page,
            PageSize: options.PageSize,
            TotalCount: totalCount
        );
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