using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LocalDBWebApiUsingEF.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Picture = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountHolderName = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<double>(type: "REAL", nullable: false),
                    UserUsername = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Users_UserUsername",
                        column: x => x.UserUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    BankAccountAccountNumber = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankAccountAccountNumber",
                        column: x => x.BankAccountAccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Address", "Email", "Name", "Password", "Phone", "Picture" },
                values: new object[,]
                {
                    { "mike", "Northbridge", "email3@gmail.com", "Mike", "mypassword3", "333-333-3333", "/images/women1.jpeg" },
                    { "mistry", "Victoria Park", "email2@gmail.com", "Mistry", "mypassword", "222-222-2222", "/images/man2.jpeg" },
                    { "sajib", "Bently", "email1@gmail.com", "Sajib", "mypassword1", "111-111-1111", "/images/man1.jpeg" }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "AccountNumber", "AccountHolderName", "Balance", "UserUsername" },
                values: new object[,]
                {
                    { 10001, "Sajib's Account", 5000.5, "sajib" },
                    { 10002, "Mistry's Account", 2500.75, "mistry" },
                    { 10003, "Mike's Account", 12000.0, "mike" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserUsername",
                table: "BankAccounts",
                column: "UserUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountAccountNumber",
                table: "Transactions",
                column: "BankAccountAccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
