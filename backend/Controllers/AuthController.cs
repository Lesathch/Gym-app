using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _repo;
    private readonly JwtService _jwt;

    public AuthController(IAuthRepository repo, JwtService jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        // 1. Buscar usuario
        var user = await _repo.GetUserByEmailAsync(request.Email);
        if (user == null)
            return Unauthorized("Credenciales inválidas");

        // 2. Verificar password (bcrypt)
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Credenciales inválidas");

        // 3. Generar tokens
        var accessToken = _jwt.GenerateAccessToken(user);
        var refreshToken = _jwt.GenerateRefreshToken();

        // 4. Guardar refresh token en DB
        await _repo.SaveRefreshTokenAsync(new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new LoginResponse
        {
            Token = accessToken,
            RefreshToken = refreshToken,
            Role = user.Role,
            Name = user.Name
        });
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponse>> Refresh(RefreshTokenRequest request)
    {
        // 1. Buscar refresh token válido
        var stored = await _repo.GetRefreshTokenAsync(request.RefreshToken);
        if (stored == null || stored.ExpiresAt < DateTime.UtcNow)
            return Unauthorized("Refresh token inválido o expirado");

        // 2. Revocar el anterior
        await _repo.RevokeRefreshTokenAsync(request.RefreshToken);

        // 3. Generar nuevos tokens
        var newAccess = _jwt.GenerateAccessToken(stored.User);
        var newRefresh = _jwt.GenerateRefreshToken();

        await _repo.SaveRefreshTokenAsync(new RefreshToken
        {
            Token = newRefresh,
            UserId = stored.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });

        return Ok(new LoginResponse
        {
            Token = newAccess,
            RefreshToken = newRefresh,
            Role = stored.User.Role,
            Name = stored.User.Name
        });
    }
}
