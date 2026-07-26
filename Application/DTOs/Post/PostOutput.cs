using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Post;

public record PostOutput(
    [Required] int Id,
    [Required, StringLength(100)] string Title,
    [Required, StringLength(100), RegularExpression("^[a-z0-9-]+$")] string Slug,
    [StringLength(500)] string? Description,
    [StringLength(100)] string? MetaTitle,
    [Url, StringLength(500)] string? CoverUrl,
    [Url, StringLength(500)] string? OgCoverUrl,
    [Required] PostStatus Status,
    [Required] DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? PublishedAt
)
{
    public static PostOutput FromEntity(
        Domain.Entities.Post post
    )
    {
        return new(
            post.Id,
            post.Title,
            post.Slug,
            post.Description,
            post.MetaTitle,
            post.CoverUrl,
            post.OgCoverUrl,
            post.Status,
            post.CreatedAt,
            post.UpdatedAt,
            post.PublishedAt
        );
    }
}