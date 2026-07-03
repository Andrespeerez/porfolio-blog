using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Application.DTOs;

namespace Application.UseCases;

public class AuthenticateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISessionManager _sessionManager;

    public AuthenticateUser(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ISessionManager sessionManager)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _sessionManager = sessionManager;
    }

    public async Task<AuthResult> ExecuteAsync(string email, string password)
    {
        User? user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return AuthResult.Fail("Credenciales incorrectas.");

        if (!_passwordHasher.Verify(user.PasswordHash, password))
            return AuthResult.Fail("Credenciales incorrectas.");

        await _sessionManager.SignInAsync(user); // crea cookie

        return AuthResult.Ok();
    }
}