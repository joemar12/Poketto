using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poketto.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJournalEntryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "JournalEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JournalEntries");
        }
    }
}
