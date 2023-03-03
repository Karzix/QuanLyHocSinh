using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Data.Migrations
{
    public partial class LopHoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LopHocId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LopHocs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaLopHoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_LopHocId",
                table: "Students",
                column: "LopHocId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_LopHocs_LopHocId",
                table: "Students",
                column: "LopHocId",
                principalTable: "LopHocs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_LopHocs_LopHocId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LopHocs");

            migrationBuilder.DropIndex(
                name: "IX_Students_LopHocId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LopHocId",
                table: "Students");
        }
    }
}
