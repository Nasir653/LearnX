using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LearnX_Server.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EnrollmentId { get; set; }

        [ForeignKey("UserId")]
        public Guid? UserId { get; set; } // Foreign key to User

        public User? User { get; set; } // Navigation property

        public Guid? CourseId { get; set; } // Foreign key to Course
        [ForeignKey("CourseId")]

        public Course? Course { get; set; } // Navigation property
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public double Progress { get; set; } = 0; // Progress as percentage (0-100)
        public bool Completed { get; set; } = false;
    }

}
