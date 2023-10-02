using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LocalDBWebApiUsingEF.Models
{
    public class BankAccount
    {
        public BankAccount(int AccountNumber, string UserUsername)
        {
            this.AccountNumber = AccountNumber;
            this.UserUsername = UserUsername!;
        }

        public int AccountNumber { get; set; }
        public string? AccountHolderName { get; set; }
        public double Balance { get; set; }
        // Foreign Key to User

        public string UserUsername { get; set; }

        // Navigation Property to User (optional but recommended for EF operations)
        [JsonIgnore]
        public virtual User? User { get; set; }


        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
