using Microsoft.EntityFrameworkCore;
using LearnX_Server.Models;

namespace LearnX_Server.Data
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Lesson> Lessons { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
    


protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User-Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course-Enrollment (One-to-Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)  // Each course has ONE Instructor
                .WithMany(u => u.Courses)   // A User (Instructor) can have MANY Courses ✅
                .HasForeignKey(c => c.InstructorId) // Foreign Key
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced

            modelBuilder.Entity<Lesson>()
               .HasOne(l => l.Course)  // Each lesson belongs to one course
               .WithMany(c => c.Lessons) // A course can have many lessons
               .HasForeignKey(l => l.CourseId) // Foreign Key
               .OnDelete(DeleteBehavior.Cascade); // Delete lessons if course is deleted

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User) // Each payment belongs to a user
                .WithMany() // No need for a payments list in User
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting user if payment exists

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Course) // Each payment is for a course
                .WithMany() // No need for a payments list in Course
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting course if payment exists





        }

    }

}