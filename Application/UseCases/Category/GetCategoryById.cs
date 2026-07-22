using Application.DTOs.Category;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class GetCategoryById
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryById(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryOutput?> ExecuteAsync(int id)
    {
        Category? existing = await _categoryRepository.GetByIdAsync(id);
        if (existing is null) return null;

        return CategoryOutput.FromEntity(existing);
    }
}