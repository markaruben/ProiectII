using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldFly.API.Migrations
{
    /// <inheritdoc />
    public partial class Migrationplmm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    departureCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arrivalCityID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    departureTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arrivalTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
