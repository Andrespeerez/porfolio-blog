using Application.UseCases;

namespace Api.Auth;

public static class Logout
{
    public static IEndpointRouteBuilder MapLogout(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/auth/logout", HandleAsync).RequireAuthorization().DisableAntiforgery();

        return routes;
    }

    public static async Task<IResult> HandleAsync(LogoutUser useCase)
    {
        await useCase.ExecuteAsync();
        return Results.Redirect("/login");
    }
}