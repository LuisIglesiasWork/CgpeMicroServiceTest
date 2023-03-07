using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cgpe.Du.Infrastructure.Migrations
{
    public partial class AddCreatorUserToProcurator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "Procurators",
                nullable: false,
                // Proceso automático del CGPE
                defaultValue: new Guid("75464AD7-021F-4703-835E-079765D9DC06"));

            migrationBuilder.CreateIndex(
                name: "IX_Procurators_CreatorUserId",
                table: "Procurators",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procurators_DirectoryUsers_CreatorUserId",
                table: "Procurators",
                column: "CreatorUserId",
                principalTable: "DirectoryUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procurators_DirectoryUsers_CreatorUserId",
                table: "Procurators");

            migrationBuilder.DropIndex(
                name: "IX_Procurators_CreatorUserId",
                table: "Procurators");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Procurators");
        }
    }
}
