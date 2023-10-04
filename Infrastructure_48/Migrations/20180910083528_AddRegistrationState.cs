using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cgpe.Du.Infrastructure.Migrations
{
    public partial class AddRegistrationState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcuratorRegistrationState",
                table: "Procurators");

            migrationBuilder.CreateTable(
                name: "RegistrationStates",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false),
                    StateName = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationStates", x => x.StateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrationStates");

            migrationBuilder.AddColumn<Guid>(
                name: "ProcuratorRegistrationState",
                table: "Procurators",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
