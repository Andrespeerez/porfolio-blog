namespace Application.DTOs;

public record AuthResult(bool Success, string? Error = null)
{
    public static AuthResult Ok() => new(true);
    public static AuthResult Fail(string error) => new(false, error);
}