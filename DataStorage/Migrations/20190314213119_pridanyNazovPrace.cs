using Microsoft.EntityFrameworkCore.Migrations;

namespace AspBlog.Migrations
{
    public partial class pridanyNazovPrace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameOfTask",
                table: "Prace",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameOfTask",
                table: "Prace");
        }
    }
}
