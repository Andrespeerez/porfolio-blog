using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Infrastructure.Persistence.Seed;

public class AdminUserSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public AdminUserSeeder(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        _environment = environment;
    }

    public async Task SeedAsync()
    {
        if (!_environment.IsDevelopment())
        {
            return;
        }

        Console.WriteLine("[Seed] Admin User iniciado...");

        var email = _configuration["Seed:AdminEmail"];
        var password = _configuration["Seed:AdminPassword"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        var existing = await _userRepository.GetByEmailAsync(email);
        if (existing is not null)
        {
            Console.WriteLine($"[Seed] Admin '{email}' ya existe.");
            return;
        }

        var user = Domain.Entities.User.Create(email, password, _passwordHasher, true);
        await _userRepository.AddAsync(user);
        Console.WriteLine($"[Seed] Admin '{email}' creado.");
    }
}