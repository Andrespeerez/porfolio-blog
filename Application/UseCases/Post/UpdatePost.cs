using Application.DTOs.Post;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class UpdatePost
{
    private readonly IPostRepository _postRepository;
    
    public UpdatePost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostOutput?> ExecuteAsync(UpdatePostInput updatePost, User user)
    {
        Post? post = await _postRepository.GetByIdAsync(updatePost.Id);
        if (post is null)
        {
            return null;
        }

        if (post.UserId != user.Id)
        {
            return null;
        }

        post.UpdatePost(
            updatePost.Slug,
            updatePost.Title,
            updatePost.Description,
            updatePost.MetaTitle,
            updatePost.CoverUrl,
            updatePost.OgCoverUrl,
            updatePost.Status
        );

        return PostOutput.FromEntity(post);
    }
}