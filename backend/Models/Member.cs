using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [Phone, MaxLength(30)]
        public string Phone { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;
        public Guid? CurrentMembershipId { get; set; }
        public Membership? CurrentMembership { get; set; }
        public ICollection<ClassEnrollment> Enrollments { get; set; } = new List<ClassEnrollment>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
