using LocalDBWebApiUsingEF.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace LocalDBWebApiUsingEF.Data {
    public class DBManager : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite(@"Data Source = BankDB.db;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //Get randomly generated users
            List<User> users = FixedSizeUserList.GetInstance().GetUsers();
            modelBuilder.Entity<User>().HasData(users);

            // AdminUser Seed Data
            List<Admin> admin = new List<Admin>
            {
                new Admin("admin")
                {
                    Name = "Admin User",
                    Email = "email0@gmail.com",
                    Address = "Sydney",
                    Phone = "000-000-0000",
                    Password = "adminpassword",
                    Picture = "/resources/images/man1.jpeg"
                }
            };
            modelBuilder.Entity<Admin>().HasData(admin);

            // BankAccount Seed Data
            List<BankAccount> bankAccounts = new List<BankAccount>();
            // Randomly associate bank accounts with users
            int accountNumber = 10000;
            for (int i = 0; i < users.Count; i++) { //create a bank account for each user currently
                bankAccounts.Add(new BankAccount(accountNumber, users[i].Username) {
                    AccountHolderName = $"{users[i].Name}'s Account",
                    Balance = new Random().NextDouble() * 10000 // random balance between 0 to 10000
                });
                accountNumber++;
            }

            // Transaction Seed Data
            List<Transaction> transactions = new List<Transaction>();
            Random random = new Random();

            int transactionIdCounter = 1; // Start the counter for TransactionId

            for (int i = 0; i < 15; i++) { //Create 15 transactions
                                           // Randomly pick a sender account
                BankAccount senderAccount = bankAccounts[random.Next(bankAccounts.Count)];

                // Randomly pick a receiver account. Ensures it's not the same as sender.
                BankAccount receiverAccount;
                do {
                    receiverAccount = bankAccounts[random.Next(bankAccounts.Count)];
                } while (senderAccount.AccountNumber == receiverAccount.AccountNumber);

                // Generate a random transaction amount that's less than the sender account's balance
                double transactionAmount = random.NextDouble() * senderAccount.Balance;

                // Creates transaction record
                transactions.Add(new Transaction {
                    TransactionId = transactionIdCounter,
                    FromAccountNumber = senderAccount.AccountNumber,
                    ToAccountNumber = receiverAccount.AccountNumber,
                    Amount = transactionAmount,
                    Description = $"Transfer of {transactionAmount:C} from {senderAccount.AccountNumber} to {receiverAccount.AccountNumber}"
                });

                // Increments TransactionId
                transactionIdCounter++;

                // Deducts amount from senders account and adds it to receiver
                senderAccount.Balance -= transactionAmount;
                receiverAccount.Balance += transactionAmount;
            }

            modelBuilder.Entity<Transaction>().HasData(transactions);




            // Set Username as the primary key for User
            modelBuilder.Entity<User>()
                    .HasKey(b => b.Username);

            // Set Username as the primary key for AdminUser
            modelBuilder.Entity<Admin>()
                    .HasKey(a => a.Username);

            // Set AccountNumber as the primary key for BankAccount
            modelBuilder.Entity<BankAccount>()
                    .HasKey(b => b.AccountNumber);

            // Set Transaction primary key
            modelBuilder.Entity<Transaction>()
                      .HasKey(t => t.TransactionId);

            // Set Transaction primary key to autoincrement
            modelBuilder.Entity<Transaction>()
                    .Property(t => t.TransactionId)
                    .ValueGeneratedOnAdd();

            // Configure the relationship between BankAccount and User
            modelBuilder.Entity<BankAccount>()
                    .HasOne(b => b.User)
                    .WithMany(u => u.BankAccounts)  // Indicates that a user can have multiple bank accounts
                    .HasForeignKey(b => b.UserUsername)
                    .OnDelete(DeleteBehavior.Cascade)  // If a user is deleted, their bank accounts are deleted as well
                    .IsRequired(true);  // UserId is required, but User navigation property is not

            // Configure the relationship between BankAccount and Transaction
            modelBuilder.Entity<BankAccount>()
                    .HasMany(b => b.Transactions)
                    .WithOne(t => t.BankAccount)
                    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BankAccount>().HasData(bankAccounts);
        }
    }
}
