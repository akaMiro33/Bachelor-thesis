using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspBlog.Migrations
{
    public partial class datumVlozenia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracaTagy_Tasks_PracaUuid",
                table: "PracaTagy");

            migrationBuilder.DropIndex(
                name: "IX_PracaTagy_PracaUuid",
                table: "PracaTagy");

            migrationBuilder.DropColumn(
                name: "PracaUuid",
                table: "PracaTagy");

            migrationBuilder.RenameColumn(
                name: "uuidPrace",
                table: "PracaTagy",
                newName: "TaskUuid");

            migrationBuilder.RenameColumn(
                name: "poznamka",
                table: "ApiKeys",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "datum",
                table: "ApiKeys",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfInsertion",
                table: "Tasks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PracaTagy_TaskUuid",
                table: "PracaTagy",
                column: "TaskUuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PracaTagy_Tasks_TaskUuid",
                table: "PracaTagy",
                column: "TaskUuid",
                principalTable: "Tasks",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracaTagy_Tasks_TaskUuid",
                table: "PracaTagy");

            migrationBuilder.DropIndex(
                name: "IX_PracaTagy_TaskUuid",
                table: "PracaTagy");

            migrationBuilder.DropColumn(
                name: "DateOfInsertion",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskUuid",
                table: "PracaTagy",
                newName: "uuidPrace");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "ApiKeys",
                newName: "poznamka");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "ApiKeys",
                newName: "datum");

            migrationBuilder.AddColumn<Guid>(
                name: "PracaUuid",
                table: "PracaTagy",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracaTagy_PracaUuid",
                table: "PracaTagy",
                column: "PracaUuid");

            migrationBuilder.AddForeignKey(
                name: "FK_PracaTagy_Tasks_PracaUuid",
                table: "PracaTagy",
                column: "PracaUuid",
                principalTable: "Tasks",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
