using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTripApi.Migrations
{
    /// <inheritdoc />
    public partial class fixingTripModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Trip",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Trip",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Trip");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Trip",
                newName: "UpdateAt");
        }
    }
}
