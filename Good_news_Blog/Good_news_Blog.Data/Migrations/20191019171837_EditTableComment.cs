using Microsoft.EntityFrameworkCore.Migrations;

namespace Good_news_Blog.Data.Migrations
{
    public partial class EditTableComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CountDislikes",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountLikes",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountDislikes",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CountLikes",
                table: "Comments");

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Comments",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
