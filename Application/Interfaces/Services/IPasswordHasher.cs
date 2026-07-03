namespace Application.Interfaces.Services;

public interface IPasswordHasher
{
    bool Verify(string hashedPassword, string password);
    string Hash(string rawPassword);
}