using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTests.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class initImagValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Images",
                schema: "usr",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                schema: "usr",
                table: "Users");
        }
    }
}
