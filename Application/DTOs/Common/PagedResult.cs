namespace Application.DTOs.Common;

public record PagedResult<T>(
    IReadOnlyList<T>? Items,
    int Page,
    int PageSize,
    int TotalCount
)
{
    public int TotalPages => PageSize > 0 ? (TotalCount + PageSize - 1) / PageSize : 0;
}

