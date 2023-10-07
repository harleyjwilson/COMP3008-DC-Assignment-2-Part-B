namespace LocalDBWebApiUsingEF.Models
{
    public class User
    {

        public User(string? Username)
        {
            this.Username = Username!;
        }

        // public User(User user)
        // {
        //     this.Username = user.Username;
        //     this.Name = user.Name;
        //     this.Email = user.Email;
        //     this.Address = user.Address;
        //     this.Phone = user.Phone;
        //     this.Picture = user.Picture;
        //     this.Password = user.Password;
        //     this.SessionID = user.SessionID;
        //     this.BankAccounts = user.BankAccounts;
        // }

        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Picture { get; set; }
        public string? Password { get; set; }
        public string? SessionID { get; set; }
        // Navigation Property for BankAccounts
        public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
    }
}
