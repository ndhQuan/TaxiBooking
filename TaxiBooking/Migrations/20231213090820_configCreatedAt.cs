using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxiBooking.Migrations
{
    /// <inheritdoc />
    public partial class configCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartPos",
                table: "Journey",
                newName: "StartAddr");

            migrationBuilder.RenameColumn(
                name: "LongStart",
                table: "Journey",
                newName: "StartLng");

            migrationBuilder.RenameColumn(
                name: "LongDes",
                table: "Journey",
                newName: "StartLat");

            migrationBuilder.RenameColumn(
                name: "LatStart",
                table: "Journey",
                newName: "EndLng");

            migrationBuilder.RenameColumn(
                name: "LatDes",
                table: "Journey",
                newName: "EndLat");

            migrationBuilder.RenameColumn(
                name: "DesPos",
                table: "Journey",
                newName: "EndAddr");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Journey",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartLng",
                table: "Journey",
                newName: "LongStart");

            migrationBuilder.RenameColumn(
                name: "StartLat",
                table: "Journey",
                newName: "LongDes");

            migrationBuilder.RenameColumn(
                name: "StartAddr",
                table: "Journey",
                newName: "StartPos");

            migrationBuilder.RenameColumn(
                name: "EndLng",
                table: "Journey",
                newName: "LatStart");

            migrationBuilder.RenameColumn(
                name: "EndLat",
                table: "Journey",
                newName: "LatDes");

            migrationBuilder.RenameColumn(
                name: "EndAddr",
                table: "Journey",
                newName: "DesPos");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Journey",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
