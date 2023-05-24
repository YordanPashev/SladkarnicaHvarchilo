using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SladkarnicaHvarchilo.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateCakeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cakes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Pieces = table.Column<int>(type: "int", nullable: false),
                    Allergens = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageFileDirectoryPath = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cakes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cakes_IsDeleted",
                table: "Cakes",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cakes");
        }
    }
}
