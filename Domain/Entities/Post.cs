namespace Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Slug { get; private set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? MetaTitle { get; set; }
    public string? CoverUrl { get; set; }
    public string? OgCoverUrl { get; set; }
    public PostStatus Status { get; set; } = PostStatus.DRAFT;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; private set; } = null!;

    public List<Category> Categories { get; set; } = new();

    public static Post Create(
        User user,
        string slug,
        string title,
        string? description,
        string? metaTitle,
        string? coverUrl,
        string? ogCoverUrl,
        PostStatus status
    )
    {
        return new()
        {
            User = user,
            UserId = user.Id,
            Slug = slug,
            Title = title,
            Description = description,
            MetaTitle = metaTitle,
            CoverUrl = coverUrl,
            OgCoverUrl = ogCoverUrl,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdatePost(
        string slug,
        string title,
        string? description,
        string? metaTitle,
        string? coverUrl,
        string? ogCoverUrl,
        PostStatus status
    )
    {
        if (status == PostStatus.PUBLISHED && Status != PostStatus.PUBLISHED)
        {
            PublishedAt = DateTime.UtcNow;
        }

        Slug = slug;
        Title = title;
        Description = description;
        MetaTitle = metaTitle;
        CoverUrl = coverUrl;
        OgCoverUrl = ogCoverUrl;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        if (DeletedAt is not null) return;

        UpdatedAt = DateTime.UtcNow;
        DeletedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (DeletedAt is null) return;

        UpdatedAt = DateTime.UtcNow;
        DeletedAt = null; 
    }

    public bool IsDeleted()
    {
        return DeletedAt is not null;
    }
}