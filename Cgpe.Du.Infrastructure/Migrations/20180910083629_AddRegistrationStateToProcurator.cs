using Microsoft.EntityFrameworkCore.Migrations;

namespace Cgpe.Du.Infrastructure.Migrations
{
    public partial class AddRegistrationStateToProcurator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationStateId",
                table: "Procurators",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Procurators_RegistrationStateId",
                table: "Procurators",
                column: "RegistrationStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procurators_RegistrationStates_RegistrationStateId",
                table: "Procurators",
                column: "RegistrationStateId",
                principalTable: "RegistrationStates",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procurators_RegistrationStates_RegistrationStateId",
                table: "Procurators");

            migrationBuilder.DropIndex(
                name: "IX_Procurators_RegistrationStateId",
                table: "Procurators");

            migrationBuilder.DropColumn(
                name: "RegistrationStateId",
                table: "Procurators");
        }
    }
}
