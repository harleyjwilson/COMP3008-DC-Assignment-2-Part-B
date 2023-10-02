using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LocalDBWebApiUsingEF.Models
{
    public class Transaction
    {
        public Transaction(int AccountNumber, double Amount)
        {
            this.AccountNumber = AccountNumber;
            this.Amount = Amount;
        }

        public int TransactionId { get; set; }
        public int AccountNumber { get; set; }
        public double Amount { get; set; }
        // Foreign Key to User

        [JsonIgnore]
        public virtual BankAccount? BankAccount { get; set; }
    }
}
