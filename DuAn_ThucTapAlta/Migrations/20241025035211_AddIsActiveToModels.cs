using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAn_ThucTapAlta.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Flights",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Documents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Documents");
        }
    }
}
