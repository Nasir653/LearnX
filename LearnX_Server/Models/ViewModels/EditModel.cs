namespace LearnX_Server.Models.ViewModels
{
    public class EditModel
    {
        public Guid id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } // "admin", "student", or "instructor"
        public string? ProfilePicture { get; set; } // Optional
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    }
}
