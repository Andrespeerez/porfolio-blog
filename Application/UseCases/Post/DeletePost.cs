using Application.DTOs.Post;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class DeletePost
{
    private readonly IPostRepository _postRepository;

    public DeletePost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostOutput?> ExecuteAsync(
        int postId,
        User user
    )
    {
        Post? post = await _postRepository.GetByIdAsync(postId);
        if (post is null || post.DeletedAt is not null)
        {
            return null;
        }

        if (!user.IsAdmin && user.Id != post.UserId)
        {
            return null;
        }

        await _postRepository.DeleteAsync(post);

        return PostOutput.FromEntity(post);        
    }
}