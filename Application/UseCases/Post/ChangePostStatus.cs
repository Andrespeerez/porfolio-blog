using Application.DTOs.Post;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class ChangePostStatus
{
    private readonly IPostRepository _postRespository;

    public ChangePostStatus(IPostRepository postRepository)
    {
        _postRespository = postRepository;
    }

    public async Task<PostOutput?> ExecuteAsync(UpdatePostInput newPostStatusChange, User user)
    {
        Post? post = await _postRespository.GetByIdAsync(newPostStatusChange.Id);
        if (post is null || post.UserId != user.Id && !user.IsAdmin)
        {
            return null;
        }

        if (post.Status == newPostStatusChange.Status)
        {
            return null;
        }

        await _postRespository.ChangeStatus(post, newPostStatusChange.Status);

        return PostOutput.FromEntity(post);
    }
}