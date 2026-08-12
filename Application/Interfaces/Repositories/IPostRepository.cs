using Application.DTOs.Common;
using Application.DTOs.Post;
using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IPostRepository
{
    Task<PagedResult<Post>?> GetPostAsync(ListPostQuery options, User? user = null, bool deleted = false);
    Task<Post?> GetByIdAsync(int id);
    Task<Post?> GetBySlugAsync(string slug);
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task ChangeStatus(Post post, PostStatus status);
    Task DeleteAsync(Post post);
    Task RestoreAsync(Post post);
}