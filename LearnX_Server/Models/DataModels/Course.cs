using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LearnX_Server.Models
{
    public class Course
    {
        public Guid CourseId { get; set; }    
        public required string Title { get; set; }  
        public string? Description { get; set; }  
        public required decimal Price { get; set; }  
        public string? Image { get; set; }    
        public required string Category { get; set; }  
        public string? Level { get; set; }  
        public double? Rating { get; set; } = 0;  
        public string? Reviews { get; set; }
        public int? TotalEnrollments { get; set; } = 0;  
        public DateTime CreatedAt { get; set; } = DateTime.Now;  
        public DateTime UpdatedAt { get; set; } = DateTime.Now;  

        // Foreign Key
        public Guid? InstructorId { get; set; }  
        [ForeignKey("InstructorId")]

        [JsonIgnore] // Prevent circular reference
        public User? Instructor { get; set; }  

        [JsonIgnore] // Prevent circular reference
        public List<Enrollment>? Enrollments { get; set; }  

        [JsonIgnore] // Prevent circular reference
        public List<Lesson>? Lessons { get; set; }    
    }
}
