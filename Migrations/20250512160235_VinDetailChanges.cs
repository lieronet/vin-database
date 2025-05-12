using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vin_db.Migrations
{
    /// <inheritdoc />
    public partial class VinDetailChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InUse",
                table: "VinDetail");

            migrationBuilder.RenameColumn(
                name: "ProcessedDate",
                table: "VinDetail",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "VinDetail",
                newName: "ProcessedDate");

            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "VinDetail",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
