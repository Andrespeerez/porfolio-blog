

namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsVisible = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Category Create(
        string name,
        string slug,
        bool isVisible,
        string? description = null
    )
    {
        return new Category() {
            Name = name,
            Slug = slug,
            Description = description,
            IsVisible = isVisible,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void UpdateCategory(
        string name,
        string slug,
        string description,
        bool isVisible
    )
    {
        this.Name = name;
        this.Slug = slug;
        this.Description = description;
        this.IsVisible = isVisible;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleVisibility()
    {
        this.IsVisible = this.IsVisible ? false : true;
    }
}