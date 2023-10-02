using LocalDBWebApiUsingEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LocalDBWebApiUsingEF.Data
{
    public class DBManager : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = BankDB.db;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Seed Data
            List<User> users = new List<User>
            {
                new User("sajib")
                {
                    Name = "Sajib",
                    Email = "email1@gmail.com",
                    Address = "Bently",
                    Phone = "111-111-1111",
                    Password = "mypassword1",
                    Picture = "/resources/images/man1.jpeg"
                },
                new User("mistry")
                {
                    Name = "Mistry",
                    Email = "email2@gmail.com",
                    Address = "Victoria Park",
                    Phone = "222-222-2222",
                    Password = "mypassword",
                    Picture = "/resources/images/man2.jpeg"
                },
                new User("mike")
                {
                    Name = "Mike",
                    Email = "email3@gmail.com",
                    Address = "Northbridge",
                    Phone = "333-333-3333",
                    Password = "mypassword3",
                    Picture = "/resources/images/women1.jpeg"
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            // BankAccount Seed Data
            List<BankAccount> bankAccounts = new List<BankAccount>
            {
                new BankAccount(10001, "sajib")
                {
                    AccountHolderName = "Sajib's Account",
                    Balance = 5000.50
                },
                new BankAccount(10002, "mistry")
                {
                    AccountHolderName = "Mistry's Account",
                    Balance = 2500.75
                },
                new BankAccount(10003, "mike")
                {
                    AccountHolderName = "Mike's Account",
                    Balance = 12000.00
                }
            };

            // Transaction Seed Data
            List<Transaction> transactions = new List<Transaction>
            {
                new Transaction(10001, 200.20),
                new Transaction(10002, 694.70)
            };

            // Set AccountNumber as the primary key for User
            modelBuilder.Entity<User>()
                .HasKey(b => b.Username);

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
