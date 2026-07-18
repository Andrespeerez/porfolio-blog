

namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Category Create(
        int id,
        string name,
        string slug,
        DateTime createdAt,
        DateTime updatedAt,
        string? description = null
    )
    {
        return new Category() {
            Id = id,
            Name = name,
            Slug = slug,
            Description = description,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };
    }

    public void UpdateCategory(
        string name,
        string slug,
        string description
    )
    {
        this.Name = name;
        this.Slug = slug;
        this.Description = description;
        this.UpdatedAt = DateTime.UtcNow;
    }
}