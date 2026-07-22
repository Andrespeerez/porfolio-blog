using Application.DTOs.Category;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class GetCategoriesBySlug
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesBySlug(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryOutput?> ExecuteAsync(string slug)
    {
        Category? existing = await _categoryRepository.GetBySlugAsync(slug);
        if (existing is null) return null;

        return CategoryOutput.FromEntity(existing);
    }


}