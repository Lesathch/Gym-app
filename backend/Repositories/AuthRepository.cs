using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly GymDbContext _context;
    public AuthRepository(GymDbContext context) => _context = context;

    public async Task<User?> GetUserByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task SaveRefreshTokenAsync(RefreshToken token)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token) =>
        await _context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == token && !r.IsRevoked);

    public async Task RevokeRefreshTokenAsync(string token)
    {
        var rt = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        if (rt != null) { rt.IsRevoked = true; await _context.SaveChangesAsync(); }
    }
}