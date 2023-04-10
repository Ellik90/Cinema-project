using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    MovieViewsShown = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxViews = table.Column<int>(type: "INTEGER", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    MovieLength = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ShowId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    DateForReservation = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.ReservationId);
                });

            migrationBuilder.CreateTable(
                name: "salons",
                columns: table => new
                {
                    SalonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalonName = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfRows = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salons", x => x.SalonId);
                });

            migrationBuilder.CreateTable(
                name: "views",
                columns: table => new
                {
                    MovieViewId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieTitle = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_views", x => x.MovieViewId);
                    table.ForeignKey(
                        name: "FK_views_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_seats_salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "salons",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_seats_SalonId",
                table: "seats",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_views_MovieId",
                table: "views",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "views");

            migrationBuilder.DropTable(
                name: "salons");

            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
