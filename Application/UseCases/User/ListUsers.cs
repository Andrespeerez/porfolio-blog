using Application.DTOs.Common;
using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class ListUsers
{
    private readonly IUserRepository _userRepository;

    public ListUsers(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<UserOutput>> ExecuteAsync(
        ListUsersQuery listUsersQuery
    )
    {
        var pagedUsers = await _userRepository.GetUsersAsync(listUsersQuery);

        var items = pagedUsers.Items.Select(UserOutput.FromEntity).ToList();

        return new PagedResult<UserOutput>(
            Items: items,
            Page: pagedUsers.Page,
            PageSize: pagedUsers.PageSize,
            TotalCount: pagedUsers.TotalCount
        );
    }
}