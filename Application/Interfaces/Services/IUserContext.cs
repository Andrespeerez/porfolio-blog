public interface IUserContext
{
    int? UserId { get; }
    bool IsAdmin { get; }
    bool IsAuthenticated { get; }
    string? Email { get; }
}