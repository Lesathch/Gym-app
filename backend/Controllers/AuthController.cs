using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;   
using Services;       

namespace Controllers  

{
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

        // POST /api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _repo.GetUserByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized("Credenciales inválidas");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Credenciales inválidas");

            var accessToken = _jwt.GenerateAccessToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

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

        // POST /api/auth/refresh
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> Refresh(RefreshTokenRequest request)
        {
            var stored = await _repo.GetRefreshTokenAsync(request.RefreshToken);
            if (stored == null || stored.ExpiresAt < DateTime.UtcNow)
                return Unauthorized("Refresh token inválido o expirado");

            await _repo.RevokeRefreshTokenAsync(request.RefreshToken);

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
}
