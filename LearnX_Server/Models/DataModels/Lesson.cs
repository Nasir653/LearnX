using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LearnX_Server.Models
{
    public class Lesson
    {
        [Key]
        public Guid LessonId { get; set; }
        public required string Title { get; set; }
        public  string? VideoUrl { get; set; } // URL of the lesson video
        public int? Duration { get; set; } // Duration in minutes

        public required Guid CourseId { get; set; } // Foreign key to Course
        [ForeignKey("CourseId")]
       
    }

}
