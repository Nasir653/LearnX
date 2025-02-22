using System.ComponentModel.DataAnnotations;

namespace LearnX_Server.Models
{

    public class User
    {

        [Key]
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; } // "admin", "student", or "instructor"
        public string? ProfilePicture { get; set; } // Optional
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public List<Enrollment>? Enrollments { get; set; } // List of courses user enrolled in
        public List<Course>? Courses { get; set; } // List of courses
    }

}
