using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.UseCases;

public class RegisterUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUser(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterResult> ExecuteAsync(string email, string rawPassword)
    {
        var existing = await _userRepository.GetByEmailAsync(email);
        if (existing is not null)
        {
            return RegisterResult.Fail("Ese email ya está registrado.");
        }

        var user = User.Create(email, rawPassword, _passwordHasher);

        await _userRepository.AddAsync(user);

        return RegisterResult.Ok(user.Id);
    }
}