#nullable disable
namespace SladkarnicaHvarchilo.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class UpdatingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CakePiecesInfo");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cakes");

            migrationBuilder.CreateTable(
                name: "PriceInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PastryId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pieces = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CakeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceInfo_Cakes_CakeId",
                        column: x => x.CakeId,
                        principalTable: "Cakes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfo_CakeId",
                table: "PriceInfo",
                column: "CakeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceInfo_IsDeleted",
                table: "PriceInfo",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceInfo");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Cakes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CakePiecesInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CakeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Piece = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CakePiecesInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CakePiecesInfo_Cakes_CakeId",
                        column: x => x.CakeId,
                        principalTable: "Cakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CakePiecesInfo_CakeId",
                table: "CakePiecesInfo",
                column: "CakeId");

            migrationBuilder.CreateIndex(
                name: "IX_CakePiecesInfo_IsDeleted",
                table: "CakePiecesInfo",
                column: "IsDeleted");
        }
    }
}
