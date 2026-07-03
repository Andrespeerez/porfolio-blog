using Application.DTOs;
using Application.UseCases;

namespace Api.Auth;

public static class Login
{
    public static IEndpointRouteBuilder MapLogin(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/auth/login", HandleAsync);
        return routes;
    }

    public static async Task<IResult> HandleAsync(
        LoginRequest request,
        AuthenticateUser useCase
    )
    {
        var result = await useCase.ExecuteAsync(request.Email, request.Password);

        return result.Success ? Results.Ok() : Results.BadRequest(result.Error);
    }
}