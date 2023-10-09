using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LocalDBWebApiUsingEF.Models {
    public class Transaction {
        public int TransactionId { get; set; }

        [Required]
        public int FromAccountNumber { get; set; }

        [Required]
        public int ToAccountNumber { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Foreign Key to User
        [JsonIgnore]
        public virtual BankAccount? BankAccount { get; set; }
        public string? Description { get; internal set; }
    }

}