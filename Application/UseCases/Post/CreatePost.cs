using Application.DTOs.Post;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class CreatePost
{
    private readonly IPostRepository _postRepository;

    public CreatePost(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<PostOutput?> ExecuteAsync(CreatePostInput newPost, User user)
    {
        var existing = await _postRepository.GetBySlugAsync(newPost.Slug);
        if (existing is not null)
        {
            return null;
        }

        var post = Post.Create(
            user,
            newPost.Slug,
            newPost.Title,
            PostStatus.DRAFT
        );

        await _postRepository.AddAsync(post);

        return PostOutput.FromEntity(post);
    }
}