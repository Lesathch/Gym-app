using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        // DbSets para todos los endpoints
        public DbSet<User> Users { get; set; } // Admins, Trainers, Receptionists
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassEnrollment> ClassEnrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PKs compuestas y relaciones FK
            modelBuilder.Entity<ClassEnrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Member)
                      .WithMany(m => m.Enrollments)
                      .HasForeignKey(e => e.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Class)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.ClassId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.MemberId, e.ClassId }).IsUnique();
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.HasOne(m => m.Member)
                      .WithOne(m => m.CurrentMembership)
                      .HasForeignKey<Membership>(m => m.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasOne(c => c.Trainer)
                      .WithMany()
                      .HasForeignKey(c => c.TrainerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Indexes para performance en queries comunes (reportes Admin)
            modelBuilder.Entity<Member>().HasIndex(m => m.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Attendance>().HasIndex(a => new { a.MemberId, a.CheckInTime });
            modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Member)
            .WithMany(m => m.Attendances)
            .HasForeignKey(a => a.MemberId)
            .OnDelete(DeleteBehavior.Cascade);

            // Soft delete opcional: agrega bool IsDeleted en entidades
            // modelBuilder.Entity<Member>().HasQueryFilter(m => !m.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }
}
