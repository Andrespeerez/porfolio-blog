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

    public void UpdateProfile(string? slug, string? fullName, string? photoUrl, string? externalWebsiteUrl, string? jobTitle, string? bio, string? educationalInstitution, JsonDocument? topics, JsonDocument? socialNetworks)
    {
        if (this.Slug is null && slug is not null)
        {
            this.Slug = slug;
        }
        this.FullName = fullName ?? this.FullName;
        this.PhotoUrl = photoUrl ?? this.PhotoUrl;
        this.ExternalWebsiteUrl = externalWebsiteUrl ?? this.ExternalWebsiteUrl;
        this.JobTitle = jobTitle ?? this.JobTitle;
        this.Bio = bio ?? this.Bio;
        this.EducationalInstitution = educationalInstitution ?? this.EducationalInstitution;
        this.Topics = topics ?? this.Topics;
        this.SocialNetworks = socialNetworks ?? this.SocialNetworks;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeEmail(string newEmail)
    {
        if(newEmail == Email) return;

        this.Email = newEmail;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string newPassword, IPasswordHasher hasher)
    {
        string newPasswordHashed = hasher.Hash(newPassword);

        this.PasswordHash = newPasswordHashed;
        this.UpdatedAt = DateTime.UtcNow;
    }
}