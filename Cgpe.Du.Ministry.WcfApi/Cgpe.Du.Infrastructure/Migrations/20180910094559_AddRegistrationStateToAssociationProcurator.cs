using Microsoft.EntityFrameworkCore.Migrations;

namespace Cgpe.Du.Infrastructure.Migrations
{
    public partial class AddRegistrationStateToAssociationProcurator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationStateId",
                table: "AssociationProcurators",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_AssociationProcurators_RegistrationStateId",
                table: "AssociationProcurators",
                column: "RegistrationStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssociationProcurators_RegistrationStates_RegistrationStateId",
                table: "AssociationProcurators",
                column: "RegistrationStateId",
                principalTable: "RegistrationStates",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssociationProcurators_RegistrationStates_RegistrationStateId",
                table: "AssociationProcurators");

            migrationBuilder.DropIndex(
                name: "IX_AssociationProcurators_RegistrationStateId",
                table: "AssociationProcurators");

            migrationBuilder.DropColumn(
                name: "RegistrationStateId",
                table: "AssociationProcurators");
        }
    }
}
