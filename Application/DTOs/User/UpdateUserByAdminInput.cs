using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User;

public record UpdateUserByAdminInput(
    [Required] int Id, 
    [Required, EmailAddress] string Email, 
    [StringLength(100, MinimumLength = 8), RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*(),.?\"":{}|<>ñÑ])\S+$")] string? Password,
    bool? IsAdmin,
    [RegularExpression("^[a-z0-9-]+$")] string? Slug, 
    [StringLength(120)] string? FullName, 
    [Url, StringLength(500)] string? PhotoUrl, 
    [Url, StringLength(500)] string? ExternalWebsiteUrl, 
    [StringLength(80)] string? JobTitle, 
    [StringLength(1000)] string? Bio, 
    [StringLength(180)] string? EducationalInstitution, 
    List<string>? Topics, 
    List<string>? SocialNetworks
);