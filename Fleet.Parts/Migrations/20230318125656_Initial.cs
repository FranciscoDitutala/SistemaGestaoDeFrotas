using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fleet.Parts.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartCategories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 524288, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCategories", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PartTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", maxLength: 524288, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "StockEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    RegisteredBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    ProvidersInfo = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(27,9)", precision: 27, scale: 9, nullable: false),
                    Documents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lines = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    RegisteredBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    RequestedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    DeliveredTo = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelledBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(2047)", maxLength: 2047, nullable: false),
                    ApprovedLines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Documents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedLines = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    UPC = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    PartTypeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1023)", maxLength: 1023, nullable: false),
                    StockQty = table.Column<decimal>(type: "decimal(27,9)", precision: 27, scale: 9, nullable: false),
                    VariantFilters = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.UPC);
                    table.ForeignKey(
                        name: "FK_Parts_PartTypes_PartTypeName",
                        column: x => x.PartTypeName,
                        principalTable: "PartTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartTypeCategory",
                columns: table => new
                {
                    PartTypeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PartCategoryName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    SubCategory = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartTypeCategory", x => new { x.PartTypeName, x.PartCategoryName, x.SubCategory });
                    table.ForeignKey(
                        name: "FK_PartTypeCategory_PartCategories_PartCategoryName",
                        column: x => x.PartCategoryName,
                        principalTable: "PartCategories",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartTypeCategory_PartTypes_PartTypeName",
                        column: x => x.PartTypeName,
                        principalTable: "PartTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartTypeName",
                table: "Parts",
                column: "PartTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_PartTypeCategory_PartCategoryName",
                table: "PartTypeCategory",
                column: "PartCategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "PartTypeCategory");

            migrationBuilder.DropTable(
                name: "StockEntry");

            migrationBuilder.DropTable(
                name: "StockOut");

            migrationBuilder.DropTable(
                name: "PartCategories");

            migrationBuilder.DropTable(
                name: "PartTypes");
        }
    }
}
