namespace Application.DTOs.Category;

public record ListCategoryQuery(
    int Page = 1,
    int PageSize = 10,
    Dictionary<string, string>? Filters = null
)
{
    public static ListCategoryQuery Create(
        int page,
        int pageSize, 
        Dictionary<string, string>? filters = null
    ) => new(Page: page, PageSize: pageSize, Filters: filters);
}