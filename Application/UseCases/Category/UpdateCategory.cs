using Application.DTOs.Category;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class UpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryOutput?> ExecuteAsync(UpdateCategoryInput updateCategoryInput)
    {
        Category? category = await _categoryRepository.GetByIdAsync(updateCategoryInput.Id);
        if (category is null) return null;

        category.UpdateCategory(
            updateCategoryInput.Name,
            updateCategoryInput.Slug,
            updateCategoryInput.Description ?? "",
            updateCategoryInput.IsVisible        
        );

        await _categoryRepository.UpdateAsync(category);
        return CategoryOutput.FromEntity(category);
    }
}