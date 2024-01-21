#nullable disable
namespace SladkarnicaHvarchilo.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AddingAdditionalNutritionProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fats",
                table: "NutritionInfo",
                newName: "SaturatedFat");

            migrationBuilder.AddColumn<string>(
                name: "Energy",
                table: "NutritionInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<double>(
                name: "Fat",
                table: "NutritionInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Energy",
                table: "NutritionInfo");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "NutritionInfo");

            migrationBuilder.RenameColumn(
                name: "SaturatedFat",
                table: "NutritionInfo",
                newName: "Fats");
        }
    }
}
