using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBacklog.Api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorForMultiUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "DateFinished",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "DateStarted",
                table: "Animes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Animes");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUserRegistered = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAnimes",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CurrentEpisodes = table.Column<int>(type: "int", nullable: false),
                    DateStarted = table.Column<DateOnly>(type: "date", nullable: true),
                    DateFinished = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnimes", x => new { x.UserId, x.AnimeId });
                    table.ForeignKey(
                        name: "FK_UserAnimes_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnimes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animes_MalId",
                table: "Animes",
                column: "MalId",
                unique: true,
                filter: "[MalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimes_AnimeId",
                table: "UserAnimes",
                column: "AnimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnimes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Animes_MalId",
                table: "Animes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Animes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFinished",
                table: "Animes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStarted",
                table: "Animes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Animes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }
    }
}
