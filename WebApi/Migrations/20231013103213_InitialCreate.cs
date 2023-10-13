﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Picture = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    SessionID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityName = table.Column<string>(type: "TEXT", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalValues = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentValues = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Picture = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    SessionID = table.Column<string>(type: "TEXT", nullable: true)
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
                    FromAccountNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ToAccountNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BankAccountAccountNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankAccountAccountNumber",
                        column: x => x.BankAccountAccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber");
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_FromAccountNumber",
                        column: x => x.FromAccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_ToAccountNumber",
                        column: x => x.ToAccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Username", "Address", "Email", "Name", "Password", "Phone", "Picture", "SessionID" },
                values: new object[] { "admin", "Sydney", "email0@gmail.com", "Admin User", "adminpassword", "000-000-0000", "/resources/images/man1.jpeg", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Address", "Email", "Name", "Password", "Phone", "Picture", "SessionID" },
                values: new object[,]
                {
                    { "areie", "9821 Elm Ave", "areie@examplemail.com", "nyjbow wmkdjpnt", "3515", "699-337-4755", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 33, 215, 248, 255, 64, 215, 97, 255, 250, 229, 29, 255, 85, 189, 25, 255, 198, 108, 204, 255, 175, 92, 20, 255, 24, 61, 109, 255, 136, 4, 6, 255, 0, 123, 87, 54, 255, 3, 58, 144, 255, 138, 195, 167, 255, 189, 4, 81, 255, 168, 153, 43, 255, 58, 249, 167, 255, 116, 175, 127, 255, 10, 190, 105, 255, 0, 52, 77, 245, 255, 212, 24, 4, 255, 105, 180, 184, 255, 162, 203, 82, 255, 138, 12, 101, 255, 67, 159, 153, 255, 4, 63, 192, 255, 32, 197, 248, 255, 0, 155, 234, 54, 255, 63, 157, 147, 255, 122, 248, 133, 255, 117, 99, 137, 255, 251, 11, 154, 255, 86, 85, 244, 255, 74, 231, 252, 255, 155, 199, 66, 255, 0, 203, 116, 133, 255, 6, 184, 236, 255, 32, 201, 242, 255, 248, 143, 75, 255, 76, 176, 23, 255, 227, 163, 126, 255, 31, 186, 248, 255, 53, 48, 148, 255, 0, 61, 216, 63, 255, 44, 251, 18, 255, 21, 104, 228, 255, 68, 61, 111, 255, 215, 253, 92, 255, 239, 196, 207, 255, 238, 161, 62, 255, 213, 212, 184, 255, 0, 82, 230, 30, 255, 115, 144, 192, 255, 177, 132, 38, 255, 6, 88, 35, 255, 175, 156, 85, 255, 222, 86, 59, 255, 208, 115, 249, 255, 105, 49, 144, 255, 0, 19, 20, 75, 255, 195, 80, 100, 255, 101, 53, 240, 255, 244, 98, 165, 255, 8, 86, 136, 255, 96, 141, 31, 255, 120, 87, 60, 255, 49, 250, 23, 255, 118, 88, 158, 104, 29, 217, 73, 43, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "bcmxb", "3679 Maple Ave", "bcmxb@examplemail.com", "brewhs itqmikif", "2980", "858-841-5434", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 234, 64, 227, 255, 219, 32, 212, 255, 101, 71, 96, 255, 141, 98, 212, 255, 40, 92, 198, 255, 74, 61, 253, 255, 221, 70, 168, 255, 180, 20, 138, 255, 0, 114, 155, 76, 255, 186, 194, 227, 255, 67, 77, 171, 255, 230, 165, 165, 255, 106, 242, 46, 255, 237, 16, 179, 255, 75, 186, 197, 255, 147, 46, 12, 255, 0, 108, 95, 19, 255, 112, 239, 245, 255, 62, 75, 115, 255, 40, 226, 49, 255, 254, 44, 61, 255, 156, 210, 75, 255, 205, 73, 180, 255, 23, 216, 2, 255, 0, 21, 235, 177, 255, 176, 110, 230, 255, 17, 148, 153, 255, 234, 161, 166, 255, 231, 201, 157, 255, 162, 112, 212, 255, 239, 128, 97, 255, 79, 251, 189, 255, 0, 169, 238, 95, 255, 237, 156, 217, 255, 121, 168, 221, 255, 232, 199, 48, 255, 45, 168, 24, 255, 19, 244, 137, 255, 109, 128, 98, 255, 247, 1, 51, 255, 0, 102, 67, 227, 255, 217, 51, 102, 255, 219, 138, 240, 255, 157, 133, 47, 255, 64, 8, 128, 255, 205, 250, 189, 255, 54, 22, 204, 255, 164, 76, 62, 255, 0, 166, 116, 61, 255, 69, 215, 55, 255, 174, 202, 174, 255, 148, 113, 77, 255, 215, 45, 239, 255, 209, 219, 51, 255, 215, 249, 61, 255, 125, 23, 183, 255, 0, 221, 231, 81, 255, 212, 52, 101, 255, 180, 203, 100, 255, 197, 111, 78, 255, 116, 133, 150, 255, 112, 159, 24, 255, 141, 97, 129, 255, 233, 37, 162, 255, 6, 131, 167, 39, 48, 85, 192, 193, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "fjoxv", "2772 Birch Ave", "fjoxv@examplemail.com", "ncaqoy athwomrw", "4419", "639-489-9812", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 199, 86, 130, 255, 208, 48, 209, 255, 232, 7, 183, 255, 50, 51, 38, 255, 234, 176, 121, 255, 14, 183, 249, 255, 98, 61, 112, 255, 71, 122, 217, 255, 0, 165, 166, 213, 255, 29, 116, 12, 255, 107, 254, 10, 255, 247, 75, 140, 255, 76, 162, 112, 255, 162, 251, 226, 255, 32, 82, 155, 255, 217, 105, 162, 255, 0, 152, 124, 152, 255, 170, 40, 91, 255, 73, 231, 107, 255, 125, 11, 19, 255, 192, 73, 20, 255, 205, 189, 176, 255, 8, 242, 241, 255, 97, 98, 216, 255, 0, 95, 163, 106, 255, 91, 236, 54, 255, 34, 178, 174, 255, 245, 254, 20, 255, 66, 228, 164, 255, 26, 247, 152, 255, 117, 75, 165, 255, 124, 229, 227, 255, 0, 187, 79, 251, 255, 124, 102, 141, 255, 229, 165, 226, 255, 194, 150, 197, 255, 10, 27, 167, 255, 43, 120, 244, 255, 142, 49, 94, 255, 122, 11, 40, 255, 0, 39, 150, 205, 255, 188, 165, 235, 255, 181, 76, 145, 255, 114, 251, 198, 255, 24, 93, 137, 255, 45, 192, 124, 255, 56, 149, 41, 255, 124, 110, 117, 255, 0, 222, 85, 68, 255, 149, 85, 68, 255, 244, 25, 23, 255, 123, 175, 47, 255, 18, 38, 183, 255, 69, 167, 175, 255, 238, 152, 24, 255, 63, 66, 143, 255, 0, 57, 130, 54, 255, 7, 102, 139, 255, 213, 163, 218, 255, 205, 22, 173, 255, 24, 195, 22, 255, 85, 41, 80, 255, 189, 82, 160, 255, 22, 142, 32, 255, 168, 102, 158, 250, 109, 192, 102, 188, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "gjyhs", "4318 Pine Rd", "gjyhs@examplemail.com", "srqzjh wlpgvrsq", "1111", "778-617-2780", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 208, 65, 4, 255, 52, 150, 202, 255, 55, 79, 193, 255, 45, 18, 242, 255, 127, 110, 61, 255, 245, 241, 199, 255, 13, 133, 6, 255, 147, 124, 203, 255, 0, 144, 122, 137, 255, 229, 70, 203, 255, 104, 22, 208, 255, 182, 24, 140, 255, 131, 69, 110, 255, 196, 225, 31, 255, 180, 143, 28, 255, 100, 163, 80, 255, 0, 87, 47, 58, 255, 108, 100, 77, 255, 12, 193, 145, 255, 184, 201, 249, 255, 192, 95, 185, 255, 254, 64, 24, 255, 51, 218, 58, 255, 58, 29, 234, 255, 0, 103, 119, 148, 255, 95, 253, 226, 255, 81, 102, 160, 255, 20, 187, 124, 255, 120, 203, 84, 255, 79, 164, 51, 255, 245, 12, 173, 255, 41, 1, 252, 255, 0, 181, 36, 31, 255, 51, 88, 166, 255, 39, 165, 243, 255, 74, 28, 108, 255, 9, 214, 207, 255, 174, 77, 5, 255, 60, 42, 130, 255, 125, 38, 194, 255, 0, 227, 218, 211, 255, 185, 78, 44, 255, 135, 196, 208, 255, 88, 62, 230, 255, 252, 3, 121, 255, 167, 72, 187, 255, 40, 186, 0, 255, 53, 87, 221, 255, 0, 113, 70, 225, 255, 188, 62, 141, 255, 189, 213, 109, 255, 84, 56, 18, 255, 148, 97, 159, 255, 212, 243, 41, 255, 63, 235, 181, 255, 14, 181, 226, 255, 0, 77, 216, 250, 255, 94, 29, 239, 255, 68, 91, 124, 255, 174, 173, 187, 255, 61, 57, 96, 255, 14, 16, 132, 255, 67, 245, 123, 255, 231, 159, 105, 255, 120, 219, 157, 110, 137, 90, 231, 173, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "govzi", "6711 Elm St", "govzi@examplemail.com", "prommw grmfbhpe", "6416", "668-610-4045", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 9, 89, 80, 255, 189, 219, 142, 255, 5, 59, 244, 255, 239, 122, 29, 255, 130, 110, 127, 255, 140, 70, 178, 255, 53, 14, 94, 255, 246, 55, 213, 255, 0, 224, 237, 104, 255, 83, 221, 76, 255, 59, 97, 75, 255, 147, 141, 196, 255, 206, 123, 250, 255, 72, 86, 171, 255, 231, 213, 121, 255, 198, 244, 232, 255, 0, 100, 114, 146, 255, 166, 146, 219, 255, 25, 250, 225, 255, 48, 140, 109, 255, 246, 45, 17, 255, 197, 187, 188, 255, 72, 247, 58, 255, 225, 167, 148, 255, 0, 66, 253, 230, 255, 13, 16, 239, 255, 236, 24, 60, 255, 150, 140, 47, 255, 146, 60, 204, 255, 16, 142, 213, 255, 222, 117, 23, 255, 135, 147, 134, 255, 0, 33, 86, 199, 255, 100, 172, 249, 255, 21, 152, 98, 255, 160, 172, 84, 255, 0, 105, 18, 255, 164, 168, 245, 255, 86, 84, 121, 255, 34, 158, 98, 255, 0, 167, 47, 150, 255, 169, 253, 227, 255, 6, 109, 243, 255, 250, 46, 53, 255, 89, 253, 246, 255, 103, 230, 74, 255, 129, 5, 92, 255, 60, 143, 134, 255, 0, 114, 153, 103, 255, 68, 103, 68, 255, 89, 84, 207, 255, 159, 180, 113, 255, 212, 208, 37, 255, 187, 233, 216, 255, 191, 196, 195, 255, 143, 63, 111, 255, 0, 71, 129, 232, 255, 83, 33, 20, 255, 27, 202, 24, 255, 125, 143, 102, 255, 247, 163, 108, 255, 111, 86, 252, 255, 69, 228, 2, 255, 65, 104, 56, 255, 186, 85, 162, 230, 171, 90, 37, 104, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "jzqug", "1698 Oak Rd", "jzqug@examplemail.com", "jkoejy mjcjaunw", "4318", "704-115-3546", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 253, 232, 14, 255, 222, 27, 208, 255, 229, 204, 43, 255, 220, 73, 169, 255, 233, 73, 243, 255, 116, 112, 56, 255, 148, 111, 200, 255, 163, 129, 3, 255, 0, 61, 46, 218, 255, 185, 235, 118, 255, 198, 181, 109, 255, 121, 116, 129, 255, 32, 79, 252, 255, 177, 125, 83, 255, 114, 52, 151, 255, 67, 173, 64, 255, 0, 141, 73, 209, 255, 89, 133, 98, 255, 7, 200, 5, 255, 100, 219, 237, 255, 6, 169, 101, 255, 76, 38, 218, 255, 130, 56, 172, 255, 199, 200, 46, 255, 0, 210, 89, 46, 255, 225, 103, 254, 255, 89, 156, 65, 255, 146, 249, 106, 255, 161, 113, 104, 255, 195, 231, 84, 255, 112, 30, 176, 255, 241, 243, 116, 255, 0, 95, 61, 241, 255, 117, 250, 38, 255, 129, 15, 212, 255, 123, 41, 117, 255, 190, 72, 146, 255, 31, 214, 55, 255, 156, 186, 37, 255, 22, 200, 248, 255, 0, 227, 174, 94, 255, 174, 162, 2, 255, 4, 177, 95, 255, 61, 199, 186, 255, 1, 229, 84, 255, 48, 50, 147, 255, 94, 239, 52, 255, 4, 146, 204, 255, 0, 173, 165, 147, 255, 169, 194, 1, 255, 171, 224, 18, 255, 184, 78, 123, 255, 229, 24, 35, 255, 221, 113, 213, 255, 203, 175, 237, 255, 222, 228, 28, 255, 0, 102, 145, 142, 255, 199, 175, 5, 255, 241, 90, 208, 255, 161, 183, 252, 255, 50, 172, 235, 255, 82, 175, 254, 255, 91, 194, 233, 255, 203, 80, 219, 255, 166, 8, 166, 173, 84, 231, 124, 98, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "oxfzk", "6192 Maple Ave", "oxfzk@examplemail.com", "iavope hvdhphrf", "8409", "102-392-7688", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 201, 25, 144, 255, 172, 153, 110, 255, 132, 235, 50, 255, 167, 116, 210, 255, 42, 116, 136, 255, 17, 18, 65, 255, 118, 201, 219, 255, 108, 63, 32, 255, 0, 177, 51, 36, 255, 91, 203, 187, 255, 1, 231, 112, 255, 252, 74, 40, 255, 9, 202, 170, 255, 43, 219, 145, 255, 221, 43, 102, 255, 141, 76, 90, 255, 0, 106, 107, 106, 255, 39, 191, 73, 255, 15, 103, 1, 255, 135, 17, 210, 255, 242, 72, 130, 255, 11, 59, 213, 255, 175, 22, 163, 255, 115, 92, 62, 255, 0, 27, 81, 212, 255, 13, 118, 105, 255, 116, 202, 88, 255, 127, 185, 171, 255, 108, 136, 224, 255, 229, 150, 225, 255, 27, 84, 116, 255, 50, 71, 0, 255, 0, 42, 123, 163, 255, 180, 165, 252, 255, 240, 7, 80, 255, 23, 199, 111, 255, 97, 216, 153, 255, 226, 247, 145, 255, 42, 22, 150, 255, 107, 182, 176, 255, 0, 135, 155, 5, 255, 118, 23, 36, 255, 113, 32, 157, 255, 178, 139, 56, 255, 32, 223, 94, 255, 215, 124, 183, 255, 212, 170, 186, 255, 99, 97, 1, 255, 0, 147, 91, 29, 255, 47, 9, 108, 255, 225, 253, 149, 255, 88, 143, 174, 255, 135, 15, 214, 255, 207, 249, 179, 255, 49, 249, 160, 255, 223, 99, 82, 255, 0, 105, 8, 6, 255, 208, 164, 104, 255, 4, 43, 216, 255, 174, 79, 89, 255, 196, 162, 139, 255, 105, 203, 76, 255, 202, 195, 203, 255, 20, 221, 136, 255, 213, 52, 155, 244, 8, 237, 185, 230, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "oyfrx", "4531 Elm Rd", "oyfrx@examplemail.com", "oznqse wcgkfwtv", "6993", "297-517-1484", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 77, 94, 39, 255, 223, 197, 251, 255, 42, 31, 74, 255, 225, 31, 144, 255, 170, 77, 196, 255, 109, 156, 65, 255, 97, 192, 135, 255, 233, 94, 111, 255, 0, 104, 3, 12, 255, 55, 38, 24, 255, 92, 170, 92, 255, 2, 24, 239, 255, 137, 206, 158, 255, 35, 65, 17, 255, 182, 93, 56, 255, 189, 240, 51, 255, 0, 96, 174, 231, 255, 27, 228, 216, 255, 54, 57, 36, 255, 172, 226, 201, 255, 11, 170, 82, 255, 141, 2, 175, 255, 177, 144, 195, 255, 31, 91, 33, 255, 0, 104, 117, 221, 255, 186, 6, 116, 255, 62, 48, 104, 255, 212, 129, 146, 255, 60, 218, 193, 255, 244, 71, 248, 255, 35, 196, 103, 255, 176, 77, 52, 255, 0, 163, 176, 227, 255, 214, 5, 43, 255, 151, 64, 38, 255, 57, 217, 123, 255, 15, 248, 237, 255, 101, 219, 18, 255, 85, 4, 109, 255, 70, 8, 199, 255, 0, 207, 106, 204, 255, 100, 22, 224, 255, 231, 5, 11, 255, 201, 149, 54, 255, 170, 42, 51, 255, 205, 182, 223, 255, 100, 144, 20, 255, 157, 164, 56, 255, 0, 28, 195, 118, 255, 19, 180, 51, 255, 101, 185, 29, 255, 154, 180, 195, 255, 60, 36, 75, 255, 175, 49, 233, 255, 51, 48, 190, 255, 220, 163, 171, 255, 0, 81, 138, 241, 255, 55, 197, 26, 255, 47, 5, 232, 255, 3, 154, 50, 255, 108, 127, 52, 255, 216, 116, 158, 255, 236, 236, 220, 255, 152, 114, 84, 255, 159, 129, 154, 111, 49, 7, 210, 114, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "pmfia", "4111 Pine Rd", "pmfia@examplemail.com", "tdqkxe sgrpvksx", "2903", "905-680-8945", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 215, 138, 30, 255, 217, 71, 195, 255, 206, 134, 220, 255, 70, 144, 222, 255, 4, 152, 2, 255, 134, 182, 158, 255, 87, 135, 181, 255, 85, 155, 13, 255, 0, 217, 126, 99, 255, 213, 96, 2, 255, 62, 86, 129, 255, 16, 88, 82, 255, 234, 211, 173, 255, 165, 32, 71, 255, 186, 235, 196, 255, 251, 218, 176, 255, 0, 99, 153, 64, 255, 76, 223, 137, 255, 199, 2, 120, 255, 159, 18, 97, 255, 242, 239, 201, 255, 80, 166, 169, 255, 33, 78, 185, 255, 41, 36, 163, 255, 0, 237, 100, 20, 255, 44, 186, 223, 255, 206, 78, 157, 255, 30, 250, 145, 255, 186, 97, 135, 255, 59, 101, 175, 255, 123, 134, 172, 255, 164, 114, 130, 255, 0, 110, 96, 111, 255, 79, 161, 146, 255, 199, 22, 10, 255, 184, 88, 119, 255, 183, 67, 38, 255, 110, 182, 184, 255, 111, 135, 138, 255, 241, 31, 191, 255, 0, 210, 126, 24, 255, 186, 162, 132, 255, 114, 135, 114, 255, 129, 163, 161, 255, 99, 14, 126, 255, 57, 1, 156, 255, 144, 47, 214, 255, 179, 94, 211, 255, 0, 56, 43, 221, 255, 6, 167, 8, 255, 38, 239, 51, 255, 155, 27, 80, 255, 176, 142, 231, 255, 221, 214, 151, 255, 95, 105, 43, 255, 103, 88, 120, 255, 0, 191, 5, 22, 255, 30, 137, 54, 255, 239, 102, 240, 255, 24, 42, 183, 255, 57, 244, 44, 255, 149, 229, 180, 255, 56, 115, 21, 255, 135, 100, 137, 255, 221, 157, 158, 83, 186, 174, 1, 163, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "qibrp", "6516 Maple Rd", "qibrp@examplemail.com", "mxxcux tvgliixn", "6312", "353-502-1330", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 15, 93, 128, 255, 223, 3, 207, 255, 236, 124, 105, 255, 90, 61, 199, 255, 14, 183, 155, 255, 1, 153, 82, 255, 101, 202, 247, 255, 107, 23, 28, 255, 0, 14, 40, 157, 255, 241, 108, 74, 255, 142, 225, 54, 255, 42, 83, 142, 255, 133, 111, 142, 255, 125, 59, 128, 255, 141, 117, 78, 255, 218, 83, 180, 255, 0, 0, 143, 42, 255, 238, 7, 77, 255, 30, 139, 71, 255, 113, 192, 108, 255, 83, 224, 117, 255, 91, 48, 40, 255, 82, 143, 33, 255, 28, 149, 151, 255, 0, 197, 23, 126, 255, 236, 62, 105, 255, 9, 18, 195, 255, 209, 179, 238, 255, 135, 87, 83, 255, 225, 136, 91, 255, 112, 176, 78, 255, 157, 250, 97, 255, 0, 79, 216, 187, 255, 161, 129, 7, 255, 19, 49, 64, 255, 76, 5, 47, 255, 244, 162, 155, 255, 249, 33, 174, 255, 210, 241, 78, 255, 90, 205, 236, 255, 0, 1, 59, 219, 255, 78, 30, 99, 255, 108, 224, 197, 255, 239, 216, 2, 255, 169, 28, 73, 255, 218, 54, 85, 255, 137, 175, 53, 255, 110, 250, 220, 255, 0, 178, 146, 76, 255, 170, 187, 54, 255, 34, 21, 126, 255, 70, 124, 226, 255, 38, 63, 72, 255, 118, 82, 151, 255, 234, 186, 21, 255, 40, 76, 4, 255, 0, 161, 129, 60, 255, 46, 80, 79, 255, 181, 22, 33, 255, 88, 68, 209, 255, 11, 60, 65, 255, 144, 98, 103, 255, 226, 96, 102, 255, 185, 137, 190, 255, 230, 126, 151, 85, 163, 140, 69, 28, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "userdonald", "Florida", "donald@realdonald.com", "Donald Trump", "userpassword", "666-666-6666", null, "null" },
                    { "userjoe", "Washington DC", "biden@potus.com", "Joe Biden", "userpassword", "777-777-7777", null, "null" },
                    { "yohup", "41 Birch Ln", "yohup@examplemail.com", "jagpzs sxqidbhi", "6707", "931-630-4069", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 22, 186, 232, 255, 136, 151, 209, 255, 229, 225, 174, 255, 7, 0, 165, 255, 82, 160, 102, 255, 146, 18, 241, 255, 138, 127, 166, 255, 65, 192, 22, 255, 0, 229, 121, 22, 255, 105, 22, 243, 255, 181, 207, 145, 255, 63, 54, 131, 255, 30, 69, 25, 255, 61, 174, 237, 255, 197, 85, 148, 255, 214, 211, 195, 255, 0, 61, 210, 202, 255, 213, 144, 181, 255, 69, 32, 253, 255, 197, 243, 74, 255, 180, 84, 241, 255, 34, 47, 56, 255, 248, 250, 142, 255, 51, 180, 149, 255, 0, 29, 148, 125, 255, 23, 54, 148, 255, 163, 206, 45, 255, 84, 145, 180, 255, 39, 14, 52, 255, 86, 212, 117, 255, 76, 166, 207, 255, 47, 10, 178, 255, 0, 23, 48, 103, 255, 243, 194, 200, 255, 62, 6, 8, 255, 5, 115, 14, 255, 85, 63, 37, 255, 79, 77, 51, 255, 137, 119, 16, 255, 129, 116, 32, 255, 0, 135, 15, 41, 255, 172, 26, 73, 255, 225, 249, 135, 255, 112, 142, 182, 255, 94, 37, 38, 255, 79, 71, 181, 255, 250, 152, 203, 255, 40, 191, 14, 255, 0, 46, 167, 87, 255, 230, 58, 209, 255, 171, 49, 29, 255, 8, 143, 25, 255, 15, 46, 211, 255, 253, 62, 177, 255, 6, 213, 130, 255, 25, 1, 163, 255, 0, 206, 84, 212, 255, 129, 112, 181, 255, 246, 133, 236, 255, 130, 144, 199, 255, 108, 243, 159, 255, 38, 105, 129, 255, 249, 146, 174, 255, 138, 148, 175, 255, 175, 160, 156, 11, 231, 249, 107, 91, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null },
                    { "yqbas", "3652 Pine St", "yqbas@examplemail.com", "twphol jjrlxjci", "2528", "628-751-6915", new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 6, 0, 0, 0, 196, 15, 190, 139, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 1, 19, 73, 68, 65, 84, 40, 83, 1, 8, 1, 247, 254, 0, 101, 15, 170, 255, 114, 245, 225, 255, 150, 175, 111, 255, 58, 128, 223, 255, 222, 186, 106, 255, 140, 253, 79, 255, 233, 135, 95, 255, 122, 173, 146, 255, 0, 53, 7, 179, 255, 245, 94, 65, 255, 34, 2, 150, 255, 78, 233, 70, 255, 166, 55, 188, 255, 120, 54, 182, 255, 43, 156, 135, 255, 122, 222, 159, 255, 0, 157, 123, 81, 255, 137, 181, 41, 255, 165, 92, 93, 255, 60, 173, 81, 255, 132, 169, 166, 255, 199, 93, 131, 255, 169, 191, 151, 255, 70, 3, 50, 255, 0, 201, 55, 161, 255, 84, 54, 230, 255, 44, 41, 236, 255, 52, 56, 4, 255, 76, 237, 254, 255, 182, 230, 191, 255, 161, 163, 67, 255, 99, 20, 1, 255, 0, 93, 28, 78, 255, 183, 154, 91, 255, 21, 98, 179, 255, 190, 184, 7, 255, 90, 25, 90, 255, 202, 167, 158, 255, 251, 113, 181, 255, 11, 170, 209, 255, 0, 73, 124, 171, 255, 253, 221, 233, 255, 235, 156, 111, 255, 77, 248, 236, 255, 230, 218, 235, 255, 126, 106, 179, 255, 197, 51, 242, 255, 34, 143, 194, 255, 0, 165, 201, 104, 255, 100, 131, 13, 255, 241, 20, 8, 255, 223, 123, 64, 255, 69, 130, 222, 255, 81, 233, 77, 255, 214, 20, 210, 255, 43, 185, 108, 255, 0, 84, 139, 183, 255, 166, 125, 28, 255, 225, 252, 173, 255, 232, 87, 251, 255, 30, 162, 121, 255, 134, 135, 132, 255, 217, 183, 17, 255, 144, 90, 153, 255, 153, 8, 164, 75, 202, 98, 201, 135, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 }, null }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "AccountNumber", "AccountHolderName", "Balance", "UserUsername" },
                values: new object[,]
                {
                    { 1, "Joe Biden's Account", 12166.0, "userjoe" },
                    { 2, "Donald Trump's Account", 40273.0, "userdonald" },
                    { 3, "oznqse wcgkfwtv's Account", 3092.7399999999998, "oyfrx" },
                    { 4, "tdqkxe sgrpvksx's Account", 21371.229999999996, "pmfia" },
                    { 5, "srqzjh wlpgvrsq's Account", 9989.8499999999985, "gjyhs" },
                    { 6, "ncaqoy athwomrw's Account", 8120.1000000000004, "fjoxv" },
                    { 7, "iavope hvdhphrf's Account", 3712.2299999999996, "oxfzk" },
                    { 8, "jagpzs sxqidbhi's Account", 1163.71, "yohup" },
                    { 9, "twphol jjrlxjci's Account", 22104.599999999999, "yqbas" },
                    { 10, "jkoejy mjcjaunw's Account", 25537.279999999999, "jzqug" },
                    { 11, "prommw grmfbhpe's Account", 112576.64, "govzi" },
                    { 12, "nyjbow wmkdjpnt's Account", 3708.7299999999996, "areie" },
                    { 13, "brewhs itqmikif's Account", 60.1200000000008, "bcmxb" },
                    { 14, "mxxcux tvgliixn's Account", 2463.6399999999994, "qibrp" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "Amount", "BankAccountAccountNumber", "Description", "FromAccountNumber", "Timestamp", "ToAccountNumber" },
                values: new object[,]
                {
                    { 1, 26.0, null, "Transfer of $26.00 from 10 to 3", 10, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6175), 3 },
                    { 2, 6975.0, null, "Transfer of $6,975.00 from 7 to 4", 7, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6214), 4 },
                    { 3, 1980.0, null, "Transfer of $1,980.00 from 3 to 9", 3, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6221), 9 },
                    { 4, 22421.0, null, "Transfer of $22,421.00 from 5 to 10", 5, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6224), 10 },
                    { 5, 5197.0, null, "Transfer of $5,197.00 from 12 to 10", 12, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6227), 10 },
                    { 6, 27099.0, null, "Transfer of $27,099.00 from 2 to 4", 2, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6231), 4 },
                    { 7, 2831.0, null, "Transfer of $2,831.00 from 10 to 3", 10, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6234), 3 },
                    { 8, 2641.0, null, "Transfer of $2,641.00 from 5 to 9", 5, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6237), 9 },
                    { 9, 891.0, null, "Transfer of $891.00 from 3 to 11", 3, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6239), 11 },
                    { 10, 24487.0, null, "Transfer of $24,487.00 from 2 to 9", 2, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6243), 9 },
                    { 11, 9027.0, null, "Transfer of $9,027.00 from 9 to 5", 9, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6246), 5 },
                    { 12, 89661.0, null, "Transfer of $89,661.00 from 1 to 11", 1, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6249), 11 },
                    { 13, 298.0, null, "Transfer of $298.00 from 3 to 11", 3, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6252), 11 },
                    { 14, 13500.0, null, "Transfer of $13,500.00 from 4 to 11", 4, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6254), 11 },
                    { 15, 702.0, null, "Transfer of $702.00 from 9 to 4", 9, new DateTime(2023, 10, 13, 10, 32, 13, 350, DateTimeKind.Utc).AddTicks(6256), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserUsername",
                table: "BankAccounts",
                column: "UserUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountAccountNumber",
                table: "Transactions",
                column: "BankAccountAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromAccountNumber",
                table: "Transactions",
                column: "FromAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToAccountNumber",
                table: "Transactions",
                column: "ToAccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}