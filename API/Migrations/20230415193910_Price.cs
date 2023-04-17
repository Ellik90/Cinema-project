using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "availableSeats",
                table: "movieViews",
                newName: "AvailableSeats");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "movies",
                newName: "MoviePrice");

            migrationBuilder.AddColumn<decimal>(
                name: "SalonPrice",
                table: "salons",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReservationPrice",
                table: "reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalonPrice",
                table: "salons");

            migrationBuilder.DropColumn(
                name: "ReservationPrice",
                table: "reservations");

            migrationBuilder.RenameColumn(
                name: "AvailableSeats",
                table: "movieViews",
                newName: "availableSeats");

            migrationBuilder.RenameColumn(
                name: "MoviePrice",
                table: "movies",
                newName: "Price");
        }
    }
}
