using Application.DTOs.Category;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class CreateCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryOutput?> ExecuteAsync(CreateCategoryInput newCategory)
    {
        var existing = _categoryRepository.GetBySlugAsync(newCategory.Slug);
        if (existing is not null)
        {
            return null;
        }

        var category = Category.Create(
            newCategory.Name,
            newCategory.Slug,
            newCategory.IsVisible,
            newCategory.Description
        );

        await _categoryRepository.AddAsync(category);

        return CategoryOutput.FromEntity(category);
    }


}