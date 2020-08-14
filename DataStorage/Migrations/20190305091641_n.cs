using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspBlog.Migrations
{
    public partial class n : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracaId",
                table: "PracaTagy");

            migrationBuilder.DropColumn(
                name: "uuidPrace",
                table: "PracaTagy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PracaId",
                table: "PracaTagy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "uuidPrace",
                table: "PracaTagy",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
