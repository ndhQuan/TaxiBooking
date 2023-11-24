using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBooking.Migrations
{
    /// <inheritdoc />
    public partial class addTaxisToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxis",
                columns: table => new
                {
                    TaxiId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxis", x => x.TaxiId);
                    table.ForeignKey(
                        name: "FK_Taxis_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Taxis_TaxiTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TaxiTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taxis_DriverId",
                table: "Taxis",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxis_TypeId",
                table: "Taxis",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxis");
        }
    }
}
