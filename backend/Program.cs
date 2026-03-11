using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Middlewares;
using Repositories;
using Services;
using System.Text;


// ============================================================
// BUILDER 
// ============================================================
var builder = WebApplication.CreateBuilder(args);


// ------------------------------------------------------------
// Database — EF Core + PostgreSQL
// ------------------------------------------------------------
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// ------------------------------------------------------------
// Authentication — JWT (Admin, Receptionist, Trainer)
// ------------------------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();


// ------------------------------------------------------------
// CORS — Frontend React
// ------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});


// ------------------------------------------------------------
// Services (Services/) 
// ------------------------------------------------------------
builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<SeedService>();


// ------------------------------------------------------------
// Repositories (Repositories/)
// ------------------------------------------------------------
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


// ------------------------------------------------------------
// Controllers + OpenAPI (Controllers)
// ------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddOpenApi();


// ============================================================
// APP
// ============================================================
var app = builder.Build();


// ------------------------------------------------------------
// OpenAPI 
// ------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// ------------------------------------------------------------
// Middlewares (Middlewares/)
// ------------------------------------------------------------
app.UseMiddleware<JwtValidationMiddleware>();


// ------------------------------------------------------------
// Pipeline HTTP
// ------------------------------------------------------------
app.UseHttpsRedirection();
app.UseCors("frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


// ------------------------------------------------------------
// Seed (Services/SeedService.cs) 
// ------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<SeedService>();
    await seed.SeedAdminAsync();
}


app.Run();
