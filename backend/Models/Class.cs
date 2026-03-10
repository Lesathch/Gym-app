using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Class
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid TrainerId { get; set; }
        public User Trainer { get; set; } = null!;
        public int MaxCapacity { get; set; } = 20;
        public ICollection<ClassEnrollment> Enrollments { get; set; } = new List<ClassEnrollment>();
    }
}
