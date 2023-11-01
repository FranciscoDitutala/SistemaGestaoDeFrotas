using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Parts.Migrations
{
    /// <inheritdoc />
    public partial class partsmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VariantFilters",
                table: "Parts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariantFilters",
                table: "Parts");
        }
    }
}
