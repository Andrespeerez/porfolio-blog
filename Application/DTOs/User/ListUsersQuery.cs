namespace Application.DTOs.User;

public record ListUsersQuery(
    int Page = 1,
    int PageSize = 10,
    Dictionary<string, string>? Filters = null
)
{
    public static ListUsersQuery Create(int page, int pageSize, Dictionary<string, string>? filters = null) => new(Page: page, PageSize: pageSize, Filters: filters);
}