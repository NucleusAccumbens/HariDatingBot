using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatingUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: true),
                    IsBeliever = table.Column<bool>(type: "bit", nullable: true),
                    IsChildfree = table.Column<bool>(type: "bit", nullable: true),
                    IsCosmopolitan = table.Column<bool>(type: "bit", nullable: true),
                    IsBdsmLover = table.Column<bool>(type: "bit", nullable: true),
                    IsMakeupUser = table.Column<bool>(type: "bit", nullable: true),
                    IsHeelsUser = table.Column<bool>(type: "bit", nullable: true),
                    IsTattooed = table.Column<bool>(type: "bit", nullable: true),
                    IsExistLover = table.Column<bool>(type: "bit", nullable: true),
                    ShaveLegs = table.Column<bool>(type: "bit", nullable: true),
                    ShaveHead = table.Column<bool>(type: "bit", nullable: true),
                    HaveSacred = table.Column<bool>(type: "bit", nullable: true),
                    OtherInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassedTheStiTest = table.Column<bool>(type: "bit", nullable: false),
                    HasPhotos = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatingUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyPart = table.Column<int>(type: "int", nullable: false),
                    PathToPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsAvatar = table.Column<bool>(type: "bit", nullable: false),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TlgUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TlgUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    DatingUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_DatingUsers_DatingUserId",
                        column: x => x.DatingUserId,
                        principalTable: "DatingUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TlgUsers",
                columns: new[] { "Id", "ChatId", "Created", "IsAdmin", "IsDeleted", "LastModified", "Username" },
                values: new object[] { 1L, 444343256L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, "noncredistka" });

            migrationBuilder.CreateIndex(
                name: "IX_Request_DatingUserId",
                table: "Request",
                column: "DatingUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "TlgUsers");

            migrationBuilder.DropTable(
                name: "DatingUsers");
        }
    }
}
