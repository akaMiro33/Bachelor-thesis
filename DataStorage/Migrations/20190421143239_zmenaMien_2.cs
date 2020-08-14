using Microsoft.EntityFrameworkCore.Migrations;

namespace AspBlog.Migrations
{
    public partial class zmenaMien_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracaTagy_Prace_PracaUuid",
                table: "PracaTagy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prace",
                table: "Prace");

            migrationBuilder.RenameTable(
                name: "Prace",
                newName: "Tasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PracaTagy_Tasks_PracaUuid",
                table: "PracaTagy",
                column: "PracaUuid",
                principalTable: "Tasks",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracaTagy_Tasks_PracaUuid",
                table: "PracaTagy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Prace");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prace",
                table: "Prace",
                column: "Uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PracaTagy_Prace_PracaUuid",
                table: "PracaTagy",
                column: "PracaUuid",
                principalTable: "Prace",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
