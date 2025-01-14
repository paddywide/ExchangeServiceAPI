﻿using ExchangeRate.Application.Contracts.Identity;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
    {
        // Define paths to exclude from token validation
        var excludedPaths = new[] { "/api/auth/login", "/api/auth/register" };

        if (excludedPaths.Any(path => context.Request.Path.StartsWithSegments(path, StringComparison.OrdinalIgnoreCase)))
        {
            // Skip middleware logic for excluded paths
            await _next(context);
            return;
        }

        // Token validation logic
        var authService = serviceProvider.GetRequiredService<IAuthService>();
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token) && await authService.IsTokenBlacklisted(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token has been revoked.");
            return;
        }

        await _next(context);
    }
}
