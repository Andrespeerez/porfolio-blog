namespace Application.DTOs.User;

public record ListUsersQuery(
    int Page = 1,
    int PageSize = 10,
    string? Search = null
);