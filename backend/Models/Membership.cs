using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Membership
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty; // "Basic", "Premium"
        public decimal Price { get; set; }
        public int DurationMonths { get; set; }
        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;
        public DateTime? ValidTo { get; set; }
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
    }
}
