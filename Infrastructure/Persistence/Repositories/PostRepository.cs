using System.Data.Entity;
using Application.DTOs.Common;
using Application.DTOs.Post;
using Application.Interfaces.Repositories;
using Domain.Entities;
using LinqKit;

namespace Infrastructure.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private AppDbContext _db;

    public PostRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<Post>?> GetPostAsync(ListPostQuery options, User? user = null, bool deleted = false)
    {

        var query = _db.Posts.AsNoTracking();

        if (options.Filters is not null && options.Filters.Count > 0)
        {
            var pred = PredicateBuilder.New<Post>();

            foreach(var filter in options.Filters)
            {
                var key = filter.Key.ToLower();
                var value =  filter.Value.Trim().ToLower();

                switch(key)
                {
                    case "title":
                        pred.Or(x => x.Title.ToLower().Contains(value));
                        break;
                    case "description":
                        pred.Or(x => x.Description!.ToLower().Contains(value));
                        break;
                }

                if (user is not null)
                {
                    pred.And(x => user.IsAdmin || x.UserId == user.Id);
                }

                pred.And(x => deleted ? x.DeletedAt != null : x.DeletedAt == null);

                query.Where(pred);
            }
        }

        int totalCount = await query.CountAsync();

        int skipCuantity = (options.Page - 1) * options.PageSize;

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip(skipCuantity)
            .Take(options.PageSize)
            .ToListAsync();

        return new PagedResult<Post>(
            Items: items,
            Page: options.Page,
            PageSize: options.PageSize,
            TotalCount: totalCount
        );
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        return _db.Posts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Post?> GetBySlugAsync(string slug)
    {
        return _db.Posts.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task AddAsync(Post post)
    {
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        _db.Posts.Update(post);
        await _db.SaveChangesAsync();
    }
}