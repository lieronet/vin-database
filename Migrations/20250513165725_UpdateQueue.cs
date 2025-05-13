using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vin_db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InUseBy",
                table: "VinQueue",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InUseDate",
                table: "VinQueue",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InUseBy",
                table: "VinQueue");

            migrationBuilder.DropColumn(
                name: "InUseDate",
                table: "VinQueue");
        }
    }
}
