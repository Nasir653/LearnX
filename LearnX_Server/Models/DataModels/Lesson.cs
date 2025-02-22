using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LearnX_Server.Models
{
    public class Lesson
    {
        [Key]
        public Guid LessonId { get; set; }
        public required string Title { get; set; }
        public required string VideoUrl { get; set; } // URL of the lesson video
        public required int Duration { get; set; } // Duration in minutes

        public Guid? CourseId { get; set; } // Foreign key to Course
        [ForeignKey("CourseId")]


        public Course? Course { get; set; } // Navigation property
    }

}
