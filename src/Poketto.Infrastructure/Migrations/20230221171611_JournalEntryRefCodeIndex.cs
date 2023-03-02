using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poketto.Infrastructure.Migrations
{
    public partial class JournalEntryRefCodeIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_RefCode_OwnerUserId",
                table: "JournalEntries",
                columns: new[] { "RefCode", "OwnerUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_RefCode_OwnerUserId",
                table: "JournalEntries");
        }
    }
}
