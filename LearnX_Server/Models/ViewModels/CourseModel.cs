using System.ComponentModel.DataAnnotations.Schema;

namespace LearnX_Server.Models.ViewModels
{
    public class CourseModel
    {


        public Guid CourseId { get; set; }    // Primary Key
        public required string Title { get; set; }  // Course title (Required)

        public string? Description { get; set; }  // Optional course description

        public Guid? InstructorId { get; set; }  // Foreign Key to User (Instructor)
        [ForeignKey("InstructorId")]


        public required decimal Price { get; set; }   // Price of the course

        public string? Image { get; set; }    // Optional: URL for course image

        public List<Lesson>? Lessons { get; set; }    // Lessons in this course
        public Guid? LessonId { get; set; }    // Lessons in this course

        public required string Category { get; set; }  // Course category (e.g., "Web Development")

        public string? Level { get; set; }  // Difficulty level: "beginner", "intermediate", "advanced"

        public double? Rating { get; set; } = 0;  // Average rating of the course
        public string? Reviews { get; set; }
        public int? TotalEnrollments { get; set; } = 0;  // Total number of students enrolled

        public DateTime CreatedAt { get; set; } = DateTime.Now;  // Timestamp of course creation

        public DateTime UpdatedAt { get; set; } = DateTime.Now;  // Timestamp of last update
    }
}
