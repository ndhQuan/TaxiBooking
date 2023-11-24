using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBooking.Migrations
{
    /// <inheritdoc />
    public partial class removeTaxiDriverId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxis_AspNetUsers_DriverId",
                table: "Taxis");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxis_TaxiTypes_TypeId",
                table: "Taxis");

            migrationBuilder.DropIndex(
                name: "IX_Taxis_DriverId",
                table: "Taxis");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Taxis");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Taxis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Taxis_TaxiTypes_TypeId",
                table: "Taxis",
                column: "TypeId",
                principalTable: "TaxiTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxis_TaxiTypes_TypeId",
                table: "Taxis");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Taxis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "Taxis",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxis_DriverId",
                table: "Taxis",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Taxis_AspNetUsers_DriverId",
                table: "Taxis",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Taxis_TaxiTypes_TypeId",
                table: "Taxis",
                column: "TypeId",
                principalTable: "TaxiTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
