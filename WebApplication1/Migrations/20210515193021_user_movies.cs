using Microsoft.EntityFrameworkCore.Migrations;

namespace imovi.Migrations
{
    public partial class user_movies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "UsersMovies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "UsersMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
