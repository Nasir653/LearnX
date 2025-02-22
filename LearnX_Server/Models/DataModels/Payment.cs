using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LearnX_Server.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; } // Foreign key to User
        [JsonIgnore]
        public User? User { get; set; } // Navigation property
        [ForeignKey("CourseId")]
        public Guid CourseId { get; set; } // Foreign key to Course
        [JsonIgnore]
        public Course? Course { get; set; } // Navigation property
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public required string PaymentMethod { get; set; } // "credit_card", "paypal", etc.
        public string? TransactionId { get; set; }
        public required string Status { get; set; } // "pending", "completed", "failed"
    }

}
