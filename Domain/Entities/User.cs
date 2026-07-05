using System.Text.Json;
using Application.Interfaces.Services;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public string? FullName { get; set; }
    public string? PhotoUrl { get; set; }
    public string? ExternalWebsiteUrl { get; set; }
    public string? JobTitle { get; set; }
    public string? Bio { get; set; }
    public string? EducationalInstitution { get; set; }
    public JsonDocument? Topics { get; set; }
    public JsonDocument? SocialNetworks { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }


    // factory
    public static User Create(string email, string rawPassword, IPasswordHasher hasher)
    {
        return new User() { Email = email, PasswordHash = hasher.Hash(rawPassword), CreatedAt = DateTime.UtcNow };
    }
}