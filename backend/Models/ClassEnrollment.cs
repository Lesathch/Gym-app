using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ClassEnrollment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public Guid ClassId { get; set; }
        public Class Class { get; set; } = null!;
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
