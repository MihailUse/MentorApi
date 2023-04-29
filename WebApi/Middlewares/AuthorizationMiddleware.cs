using System.Security.Claims;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Interfaces;

namespace WebApi.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IDatabaseContext database, ICurrentUserService currentUserService)
    {
        if (httpContext.User.Claims.Any())
        {
            var userIdString = httpContext.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value;
            if (!Guid.TryParse(userIdString, out var userId))
                throw new UnauthorizedException("Invalid token");

            var user = await database.Users.FindAsync(userId);
            if (user == default)
                throw new UnauthorizedException("Invalid token");

            currentUserService.User = user;
        }

        await _next(httpContext);
    }
}

public static class AuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationMiddleware>();
    }
}