namespace Application.DTOs.Post;

public record ListPostQuery(
    int Page = 1,
    int PageSize = 10,
    Dictionary<string, string>? Filters = null
)
{
    public static ListPostQuery Create(
        int page,
        int pageSize, 
        Dictionary<string, string>? filters = null
    ) => new(Page: page, PageSize: pageSize, Filters: filters);
}