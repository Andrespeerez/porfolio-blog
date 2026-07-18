using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Category;

public record CategoryOutput(
    [Required] int Id,
    [Required, StringLength(100)] string Name,
    [Required, StringLength(100)] string Slug,
    bool IsVisible,
    [StringLength(500)] string? Description
)
{
    public static CategoryOutput FromEntity(
        Domain.Entities.Category category
    )
    {
        return new(
            category.Id,
            category.Name,
            category.Slug,
            category.IsVisible,
            category.Description
        );
    }
}