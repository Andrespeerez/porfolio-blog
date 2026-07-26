using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Post;

public record UpdatePostInput(
    [Required] int Id,
    [Required, StringLength(100)] string Title,
    [Required, StringLength(100), RegularExpression("^[a-z0-9-]+$")] string Slug,
    [Required] PostStatus Status,
    [Required] DateTime CreatedAt
);