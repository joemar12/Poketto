using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poketto.Infrastructure.Migrations
{
    public partial class JournalEntryTableRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_Accounts_AccountId",
                table: "JournalEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_TransactionJournals_TransactionJournalId",
                table: "JournalEntries");

            migrationBuilder.DropTable(
                name: "TransactionJournals");

            migrationBuilder.DropTable(
                name: "TransactionGroups");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_AccountId",
                table: "JournalEntries");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_TransactionJournalId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "TransactionJournalId",
                table: "JournalEntries");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JournalEntries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "JournalEntryDate",
                table: "JournalEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserId",
                table: "JournalEntries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefCode",
                table: "JournalEntries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JournalEntryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    JournalEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalEntryItems_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalEntryItems_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryItems_AccountId",
                table: "JournalEntryItems",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryItems_JournalEntryId",
                table: "JournalEntryItems",
                column: "JournalEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalEntryItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "JournalEntryDate",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "RefCode",
                table: "JournalEntries");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "JournalEntries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "JournalEntries",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionJournalId",
                table: "JournalEntries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TransactionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionJournals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionJournals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionJournals_TransactionGroups_TransactionGroupId",
                        column: x => x.TransactionGroupId,
                        principalTable: "TransactionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_AccountId",
                table: "JournalEntries",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_TransactionJournalId",
                table: "JournalEntries",
                column: "TransactionJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionJournals_TransactionGroupId",
                table: "TransactionJournals",
                column: "TransactionGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_Accounts_AccountId",
                table: "JournalEntries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_TransactionJournals_TransactionJournalId",
                table: "JournalEntries",
                column: "TransactionJournalId",
                principalTable: "TransactionJournals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
