using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class ConnectionBetweenUserAndMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecifiedMovies_Users_UserId",
                table: "SpecifiedMovies");

            migrationBuilder.DropIndex(
                name: "IX_SpecifiedMovies_UserId",
                table: "SpecifiedMovies");

            migrationBuilder.AddColumn<int>(
                name: "SpecifiedMovies",
                table: "SpecifiedMovies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecifiedMovies_SpecifiedMovies",
                table: "SpecifiedMovies",
                column: "SpecifiedMovies");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecifiedMovies_Users_SpecifiedMovies",
                table: "SpecifiedMovies",
                column: "SpecifiedMovies",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecifiedMovies_Users_SpecifiedMovies",
                table: "SpecifiedMovies");

            migrationBuilder.DropIndex(
                name: "IX_SpecifiedMovies_SpecifiedMovies",
                table: "SpecifiedMovies");

            migrationBuilder.DropColumn(
                name: "SpecifiedMovies",
                table: "SpecifiedMovies");

            migrationBuilder.CreateIndex(
                name: "IX_SpecifiedMovies_UserId",
                table: "SpecifiedMovies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecifiedMovies_Users_UserId",
                table: "SpecifiedMovies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
