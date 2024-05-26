using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payment.Service.Api.Migrations
{
    /// <inheritdoc />
    public partial class addsolt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Solt",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solt",
                table: "Users");
        }
    }
}
