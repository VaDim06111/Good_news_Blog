using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Good_news_Blog.Data.Migrations
{
    public partial class ChangeTypeOfIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments");

            migrationBuilder.AlterColumn<double>(
                name: "IndexOfPositive",
                table: "News",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<Guid>(
                name: "NewsId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments");

            migrationBuilder.AlterColumn<byte>(
                name: "IndexOfPositive",
                table: "News",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<Guid>(
                name: "NewsId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_News_NewsId",
                table: "Comments",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
