using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectpolsl.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostEditDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EditDate",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "Posts");
        }
    }
}
