using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cgpe.Du.Infrastructure.Migrations
{
    public partial class AddCreatorAssociationToProcurator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorAssociationId",
                table: "Procurators",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Procurators",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procurators_CreatorAssociationId",
                table: "Procurators",
                column: "CreatorAssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procurators_Associations_CreatorAssociationId",
                table: "Procurators",
                column: "CreatorAssociationId",
                principalTable: "Associations",
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procurators_Associations_CreatorAssociationId",
                table: "Procurators");

            migrationBuilder.DropIndex(
                name: "IX_Procurators_CreatorAssociationId",
                table: "Procurators");

            migrationBuilder.DropColumn(
                name: "CreatorAssociationId",
                table: "Procurators");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Procurators");
        }
    }
}
