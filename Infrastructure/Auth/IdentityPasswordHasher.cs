using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth;

public class IdentityPasswordHasher : IPasswordHasher
{

    private readonly IPasswordHasher<User> _inner;

    public IdentityPasswordHasher(IPasswordHasher<User> inner)
    {
        _inner = inner;
    }

    public bool Verify(string hashedPassword, string password)
    {
        var result = _inner.VerifyHashedPassword(new User(), hashedPassword, password);

        return result is PasswordVerificationResult.Success;
    }

    public string Hash(string rawPassword)
    {
        return _inner.HashPassword(new User(), rawPassword);
    }
}