using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Service.Api.Migrations
{
    /// <inheritdoc />
    public partial class Changedbrootforcecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLocked",
                table: "Users",
                newName: "BadRequestCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BadRequestCount",
                table: "Users",
                newName: "IsLocked");
        }
    }
}
