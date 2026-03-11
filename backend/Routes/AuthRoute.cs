using Controllers;
namespace Routes
{
    public static class AuthRoute
    {
        public static void MapAuthRoutes(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth").WithTags("Auth");

            group.MapPost("/login", (HttpContext ctx) => ctx.RequestServices
                .GetRequiredService<AuthController>());
            // Los routes con Controllers convencionales se mapean solos con MapControllers()
            // Este archivo es para documentación y referencia de endpoints
        }
    }
}
