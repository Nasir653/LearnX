using System.ComponentModel.DataAnnotations;

namespace LearnX_Server.Models.ViewModels
{
    public class SignUp
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
    }

}
