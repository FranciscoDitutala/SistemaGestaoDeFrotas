using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Transport.Migrations
{
    /// <inheritdoc />
    public partial class alterandoVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "OrgaoId",
                table: "Vehicles",
                newName: "LastAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastAssignmentId",
                table: "Vehicles",
                newName: "OrgaoId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
