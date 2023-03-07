using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cgpe.Du.Infrastructure.Migrations
{
    public partial class AddCreatorUserToAssociationProcurator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorUserId",
                table: "AssociationProcurators",
                nullable: false,
                defaultValue: new Guid("75464AD7-021F-4703-835E-079765D9DC06"));

            migrationBuilder.CreateIndex(
                name: "IX_AssociationProcurators_CreatorUserId",
                table: "AssociationProcurators",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociationProcurators_DirectoryUsers_CreatorUserId",
                table: "AssociationProcurators",
                column: "CreatorUserId",
                principalTable: "DirectoryUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociationProcurators_DirectoryUsers_CreatorUserId",
                table: "AssociationProcurators");

            migrationBuilder.DropIndex(
                name: "IX_AssociationProcurators_CreatorUserId",
                table: "AssociationProcurators");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AssociationProcurators");
        }
    }
}
