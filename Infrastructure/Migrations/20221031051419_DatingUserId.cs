using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class DatingUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_DatingUsers_DatingUserId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Request",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Requests");

            migrationBuilder.RenameIndex(
                name: "IX_Request_DatingUserId",
                table: "Requests",
                newName: "IX_Requests_DatingUserId");

            migrationBuilder.AlterColumn<long>(
                name: "DatingUserId",
                table: "Requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_DatingUsers_DatingUserId",
                table: "Requests",
                column: "DatingUserId",
                principalTable: "DatingUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_DatingUsers_DatingUserId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Request");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_DatingUserId",
                table: "Request",
                newName: "IX_Request_DatingUserId");

            migrationBuilder.AlterColumn<long>(
                name: "DatingUserId",
                table: "Requests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Request",
                table: "Requests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_DatingUsers_DatingUserId",
                table: "Requests",
                column: "DatingUserId",
                principalTable: "DatingUsers",
                principalColumn: "Id");
        }
    }
}
