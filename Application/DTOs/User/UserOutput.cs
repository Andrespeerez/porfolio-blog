using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User;

public record UserOutput(
    [Required] int Id, 
    [Required, EmailAddress] string Email,
    bool IsAdmin,
    [RegularExpression("^[a-z0-9-]+$")] string? Slug, 
    [StringLength(120)] string? FullName,
    [Url, StringLength(500)] string? PhotoUrl, 
    [Url, StringLength(500)] string? ExternalWebsiteUrl, 
    [StringLength(80)] string? JobTitle, 
    [StringLength(1000)] string? Bio, 
    [StringLength(180)] string? EducationalInstitution, 
    List<string>? Topics, 
    List<string>? SocialNetworks, 
    DateTime CreatedAt, 
    DateTime? UpdatedAt, 
    DateTime? DeletedAt
)
{
    public static UserOutput FromEntity(Domain.Entities.User user)
    {
        return new(
            user.Id,
            user.Email,
            user.IsAdmin,
            user.Slug,
            user.FullName,
            user.PhotoUrl,
            user.ExternalWebsiteUrl,
            user.JobTitle,
            user.Bio,
            user.EducationalInstitution,
            user.Topics,
            user.SocialNetworks,
            user.CreatedAt,
            user.UpdatedAt,
            user.DeletedAt
        );
    }
}