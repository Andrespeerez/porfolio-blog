using System.ComponentModel.DataAnnotations;
using Application.Validators;

namespace Application.DTOs.User;

public record CreateUserByAdminInput(
    [Required, EmailAddress] string Email, 
    [property: Required, Password] string Password,
    bool? IsAdmin,
    [RegularExpression("^[a-z0-9-]+$")] string? Slug, 
    [StringLength(120)]  string? FullName, 
    [Url, StringLength(500)] string? PhotoUrl, 
    [Url, StringLength(500)] string? ExternalWebsiteUrl, 
    [StringLength(80)] string? JobTitle, 
    [StringLength(1000)] string? Bio, 
    [StringLength(180)] string? EducationalInstitution, 
    List<string>? Topics, 
    List<string>? SocialNetworks
);