using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspBlog.Migrations
{
    public partial class Nova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "keyValue1",
                table: "Prace");

            migrationBuilder.DropColumn(
                name: "keyValue2",
                table: "Prace");

            migrationBuilder.DropColumn(
                name: "keyValue3",
                table: "Prace");

            migrationBuilder.DropColumn(
                name: "keyValue4",
                table: "Prace");

            migrationBuilder.DropColumn(
                name: "keyValue5",
                table: "Prace");

            migrationBuilder.DropColumn(
                name: "keyValue6",
                table: "Prace");

            migrationBuilder.CreateTable(
                name: "PracaTagy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    uuidPrace = table.Column<Guid>(nullable: false),
                    PracaId = table.Column<int>(nullable: false),
                    PracaUuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracaTagy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracaTagy_Prace_PracaUuid",
                        column: x => x.PracaUuid,
                        principalTable: "Prace",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PracaTagy_PracaUuid",
                table: "PracaTagy",
                column: "PracaUuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PracaTagy");

            migrationBuilder.AddColumn<string>(
                name: "keyValue1",
                table: "Prace",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyValue2",
                table: "Prace",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyValue3",
                table: "Prace",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyValue4",
                table: "Prace",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyValue5",
                table: "Prace",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyValue6",
                table: "Prace",
                nullable: true);
        }
    }
}
