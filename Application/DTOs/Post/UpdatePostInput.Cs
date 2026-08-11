using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Post;

public record UpdatePostInput(
    [Required] int Id,
    [Required, StringLength(100)] string Title,
    [Required, StringLength(100), RegularExpression("^[a-z0-9-]+$")] string Slug,
    [Required] PostStatus Status,
    [StringLength(500)] string? Description,
    [StringLength(100)] string? MetaTitle,
    [StringLength(100)] string? CoverUrl,
    [StringLength(100)] string? OgCoverUrl,
    [Required] DateTime CreatedAt
);