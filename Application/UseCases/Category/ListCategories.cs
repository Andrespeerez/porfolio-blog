using Application.DTOs.Category;
using Application.DTOs.Common;
using Application.Interfaces.Repositories;

namespace Application.UseCases;

public class ListCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<PagedResult<CategoryOutput>> ExecuteAsync(ListCategoryQuery options)
    {
        var pagedCategories = await _categoryRepository.GetCategoriesAsync(options);

        var items = pagedCategories?.Items?.Select(CategoryOutput.FromEntity).ToList();

        return new PagedResult<CategoryOutput>(
            Items: items,
            Page: pagedCategories?.Page ?? 1,
            PageSize: pagedCategories?.PageSize ?? 10, 
            TotalCount: pagedCategories?.TotalCount ?? 0
        );
    }
}