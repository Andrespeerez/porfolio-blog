using Application.DTOs.Common;
using Application.DTOs.Post;
using Application.Interfaces.Repositories;

namespace Application.UseCases;

public class ListPost
{
    private readonly IPostRepository _postRepository;

    public ListPost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PagedResult<PostOutput>> ExecuteAsync(ListPostQuery options)
    {
        var pagedPosts = await _postRepository.GetPostAsync(options);

        var items = pagedPosts?.Items?.Select(PostOutput.FromEntity).ToList();

        return new PagedResult<PostOutput>(
            Items: items,
            Page: pagedPosts?.Page ?? 1,
            PageSize: pagedPosts?.PageSize ?? 10,
            TotalCount: pagedPosts?.TotalCount ?? 0
        );
    }
}