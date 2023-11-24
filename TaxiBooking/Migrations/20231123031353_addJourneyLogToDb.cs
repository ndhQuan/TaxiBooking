using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBooking.Migrations
{
    /// <inheritdoc />
    public partial class addJourneyLogToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StartPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatStart = table.Column<float>(type: "real", nullable: false),
                    LongStart = table.Column<float>(type: "real", nullable: false),
                    DesPos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LatDes = table.Column<float>(type: "real", nullable: false),
                    LongDes = table.Column<float>(type: "real", nullable: false),
                    Distance = table.Column<float>(type: "real", nullable: false),
                    TimeStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journey_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Journey_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Journey_Taxis_LicensePlate",
                        column: x => x.LicensePlate,
                        principalTable: "Taxis",
                        principalColumn: "TaxiId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journey_CustomerId",
                table: "Journey",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Journey_DriverId",
                table: "Journey",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Journey_LicensePlate",
                table: "Journey",
                column: "LicensePlate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journey");
        }
    }
}
