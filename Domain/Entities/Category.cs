

namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsVisible { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<Post> Posts { get; set; } = new();

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
        if (slug != "") this.Slug = slug;

        this.Name = name;
        this.Description = description;
        this.IsVisible = isVisible;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleVisibility()
    {
        this.IsVisible = this.IsVisible ? false : true;
    }
}