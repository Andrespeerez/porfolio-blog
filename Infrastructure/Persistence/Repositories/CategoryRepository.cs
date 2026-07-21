using Microsoft.EntityFrameworkCore;
using Application.DTOs.Category;
using Application.DTOs.Common;
using Application.Interfaces.Repositories;
using Domain.Entities;
using LinqKit;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PagedResult<Category>> GetCategoriesAsync(ListCategoryQuery options)
    {
        var query = _db.Categories.AsNoTracking();

        if (options.Filters is not null && options.Filters.Count > 0)
        {
            var pred = PredicateBuilder.New<Category>();

            foreach(var filter in options.Filters)
            {
                var key = filter.Key.ToLower();
                var value = filter.Value.Trim().ToLower();

                switch(key)
                {
                    case "name":
                        pred.Or(x => x.Name.ToLower().Contains(value));
                        break;
                    case "description":
                        pred.Or(x => x.Description!.ToLower().Contains(value));
                        break;
                }

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

        return new PagedResult<Category>(
            Items: items,
            Page: options.Page,
            PageSize: options.PageSize,
            TotalCount: totalCount
        );
    }

    public Task<Category?> GetByIdAsync(int id)
    {
        return _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Category?> GetBySlugAsync(string slug)
    {
        return _db.Categories.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task AddAsync(Category category)
    {
        await _db.Categories.AddAsync(category);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
    }
}