using Application.Interfaces.Services;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.UseCases;

public class CreateUserByAdmin
{
    IUserRepository _userRepository;

    public CreateUserByAdmin(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync()
    {
        //User? user =  User.Create()
    }
}