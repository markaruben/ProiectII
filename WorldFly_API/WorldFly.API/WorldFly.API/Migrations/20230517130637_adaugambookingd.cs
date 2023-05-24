using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldFly.API.Migrations
{
    /// <inheritdoc />
    public partial class adaugambookingd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "arrivalCity",
                table: "Flight",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arrivalCity",
                table: "Flight");
        }
    }
}
