using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Category;

public record UpdateCategoryInput
(
    [Required] int Id,
    [Required, StringLength(100)] string Name,
    [Required, StringLength(100)] string Slug,
    bool IsVisible,
    [StringLength(500)] string? Description
);