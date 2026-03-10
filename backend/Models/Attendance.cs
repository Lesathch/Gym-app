using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Attendance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public DateTime CheckInTime { get; set; } = DateTime.UtcNow;
        public DateTime? CheckOutTime { get; set; }
    }
    
}
