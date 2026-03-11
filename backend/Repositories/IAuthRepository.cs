using Models;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task SaveRefreshTokenAsync(RefreshToken token);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RevokeRefreshTokenAsync(string token);
}

