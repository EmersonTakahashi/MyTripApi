using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTripApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedFKToDoBeforeTripAndTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TripId",
                table: "ToDoBeforeTrip",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ToDoBeforeTrip_TripId",
                table: "ToDoBeforeTrip",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoBeforeTrip_Trip_TripId",
                table: "ToDoBeforeTrip",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoBeforeTrip_Trip_TripId",
                table: "ToDoBeforeTrip");

            migrationBuilder.DropIndex(
                name: "IX_ToDoBeforeTrip_TripId",
                table: "ToDoBeforeTrip");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "ToDoBeforeTrip");
        }
    }
}
