using System.Net;
using System.Text.Json;

namespace Middlewares
{
    public class JwtValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtValidationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // 401 — Token inválido/expirado
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.ContentType = "application/json";
                    var response = new { error = "No autorizado. Token inválido o expirado." };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }

                // 403 — Sin permisos para el rol
                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";
                    var response = new { error = "Acceso denegado. Tu rol no tiene permiso." };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new { error = "Error interno del servidor.", detail = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}