using Application.DTOs.Category;
using Application.DTOs.Common;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<PagedResult<Category>> GetCategoriesAsync(ListCategoryQuery options);

    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetBySlugAsync(string slug);

    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
}