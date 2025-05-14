using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vin_db.Migrations
{
    /// <inheritdoc />
    public partial class AddVinDetailColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BedLength",
                table: "VinDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BodyClass",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomMotorcycleType",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Doors",
                table: "VinDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ErrorCode",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ErrorText",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ModelYear",
                table: "VinDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MotorcycleChassisType",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfWheels",
                table: "VinDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TrackWidth",
                table: "VinDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VehicleType",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "WheelBaseFrom",
                table: "VinDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WheelBaseTo",
                table: "VinDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "WheelBaseType",
                table: "VinDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Windows",
                table: "VinDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedLength",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "BodyClass",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "CustomMotorcycleType",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "Doors",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "ErrorCode",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "ErrorText",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "ModelYear",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "MotorcycleChassisType",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "NumberOfWheels",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "TrackWidth",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "WheelBaseFrom",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "WheelBaseTo",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "WheelBaseType",
                table: "VinDetail");

            migrationBuilder.DropColumn(
                name: "Windows",
                table: "VinDetail");
        }
    }
}
