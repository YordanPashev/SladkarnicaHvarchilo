#nullable disable
namespace SladkarnicaHvarchilo.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class UpdateColName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileDirectoryPath",
                table: "Cakes",
                newName: "ImageFileName");

            migrationBuilder.CreateTable(
                name: "Desserts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Allergens = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageFileDirectoryPath = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Desserts");

            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                table: "Cakes",
                newName: "ImageFileDirectoryPath");
        }
    }
}
