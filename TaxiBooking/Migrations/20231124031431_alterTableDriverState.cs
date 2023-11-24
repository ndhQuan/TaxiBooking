using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBooking.Migrations
{
    /// <inheritdoc />
    public partial class alterTableDriverState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriveState_AspNetUsers_DriverId",
                table: "DriveState");

            migrationBuilder.DropForeignKey(
                name: "FK_DriveState_Taxis_BienSoXe",
                table: "DriveState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriveState",
                table: "DriveState");

            migrationBuilder.RenameTable(
                name: "DriveState",
                newName: "DriverState");

            migrationBuilder.RenameIndex(
                name: "IX_DriveState_BienSoXe",
                table: "DriverState",
                newName: "IX_DriverState_BienSoXe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverState",
                table: "DriverState",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverState_AspNetUsers_DriverId",
                table: "DriverState",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverState_Taxis_BienSoXe",
                table: "DriverState",
                column: "BienSoXe",
                principalTable: "Taxis",
                principalColumn: "TaxiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverState_AspNetUsers_DriverId",
                table: "DriverState");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverState_Taxis_BienSoXe",
                table: "DriverState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverState",
                table: "DriverState");

            migrationBuilder.RenameTable(
                name: "DriverState",
                newName: "DriveState");

            migrationBuilder.RenameIndex(
                name: "IX_DriverState_BienSoXe",
                table: "DriveState",
                newName: "IX_DriveState_BienSoXe");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriveState",
                table: "DriveState",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriveState_AspNetUsers_DriverId",
                table: "DriveState",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriveState_Taxis_BienSoXe",
                table: "DriveState",
                column: "BienSoXe",
                principalTable: "Taxis",
                principalColumn: "TaxiId");
        }
    }
}
