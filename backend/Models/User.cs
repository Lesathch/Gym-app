using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Role { get; set; } = "Receptionist"; // "Admin", "Trainer", "Receptionist"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
