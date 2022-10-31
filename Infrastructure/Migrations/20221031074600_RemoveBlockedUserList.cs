using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RemoveBlockedUserList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatingUsers_DatingUsers_DatingUserId",
                table: "DatingUsers");

            migrationBuilder.DropIndex(
                name: "IX_DatingUsers_DatingUserId",
                table: "DatingUsers");

            migrationBuilder.DropColumn(
                name: "DatingUserId",
                table: "DatingUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DatingUserId",
                table: "DatingUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatingUsers_DatingUserId",
                table: "DatingUsers",
                column: "DatingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DatingUsers_DatingUsers_DatingUserId",
                table: "DatingUsers",
                column: "DatingUserId",
                principalTable: "DatingUsers",
                principalColumn: "Id");
        }
    }
}
