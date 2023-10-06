namespace WebApi.Models
{
    public class TransferDto
    {
        public int FromAccountNumber { get; set; }
        public int ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
