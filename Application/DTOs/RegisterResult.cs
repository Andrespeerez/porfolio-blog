namespace Application.DTOs;

public record RegisterResult(bool Success, int? NewId = null, string? Error = null)
{
    public static RegisterResult Ok(int newId) => new (true, newId);
    public static RegisterResult Fail(string error) => new(false, Error: error);
}