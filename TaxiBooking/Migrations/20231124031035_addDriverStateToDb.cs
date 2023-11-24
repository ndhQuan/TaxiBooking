using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBooking.Migrations
{
    /// <inheritdoc />
    public partial class addDriverStateToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DriveState",
                columns: table => new
                {
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    latCurrent = table.Column<float>(type: "real", nullable: false),
                    longCurrent = table.Column<float>(type: "real", nullable: false),
                    TinhTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    BienSoXe = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriveState", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_DriveState_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriveState_Taxis_BienSoXe",
                        column: x => x.BienSoXe,
                        principalTable: "Taxis",
                        principalColumn: "TaxiId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriveState_BienSoXe",
                table: "DriveState",
                column: "BienSoXe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriveState");
        }
    }
}
