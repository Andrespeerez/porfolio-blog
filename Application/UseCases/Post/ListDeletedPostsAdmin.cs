using Application.DTOs.Common;
using Application.DTOs.Post;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class ListDeletedPostAdmin
{
    private readonly IPostRepository _postRepository;

    public ListDeletedPostAdmin(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PagedResult<PostOutput>> ExecuteAsync(ListPostQuery options, User user)
    {
        var pagedPosts = await _postRepository.GetPostAsync(options, user, true);

        var items = pagedPosts?.Items?.Select(PostOutput.FromEntity).ToList();

        return new PagedResult<PostOutput>(
            Items: items,
            Page: pagedPosts?.Page ?? 1,
            PageSize: pagedPosts?.PageSize ?? 10,
            TotalCount: pagedPosts?.TotalCount ?? 0
        );
    }
}