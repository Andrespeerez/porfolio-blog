using Application.Interfaces.Services;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;


    // factory
    public static User Create(string email, string rawPassword, IPasswordHasher hasher)
    {
        return new User() { Email = email, PasswordHash = hasher.Hash(rawPassword) };
    }
}