using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projectpolsl.Migrations
{
    /// <inheritdoc />
    public partial class AddPostSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PostSections_PostId",
                table: "PostSections",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostSections_Posts_PostId",
                table: "PostSections",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostSections_Posts_PostId",
                table: "PostSections");

            migrationBuilder.DropIndex(
                name: "IX_PostSections_PostId",
                table: "PostSections");
        }
    }
}
