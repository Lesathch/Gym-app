using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace Services
{
    public class SeedService
    {
        private readonly GymDbContext _context;

        public SeedService(GymDbContext context) => _context = context;

        public async Task SeedAdminAsync()
        {
            if (!await _context.Users.AnyAsync(u => u.Role == "Admin"))
            {
                _context.Users.Add(new User
                {
                    Name = "Admin",
                    Email = "admin@gym.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin1234!"),
                    Role = "Admin"
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
