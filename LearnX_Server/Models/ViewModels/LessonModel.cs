using System.ComponentModel.DataAnnotations;

namespace LearnX_Server.Models.ViewModels
{
    public class LessonModel
    {
        [Key]
        public Guid LessonId { get; set; }
        public required string Title { get; set; }
  
        public  int? Duration { get; set; } // Duration in minutes

        public required Guid CourseId { get; set; } // Foreign key to Course

     

    }
}
