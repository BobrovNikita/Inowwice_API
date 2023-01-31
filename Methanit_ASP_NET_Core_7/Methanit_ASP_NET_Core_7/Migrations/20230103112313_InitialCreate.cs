using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MethanitASPNETCore7.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fridge_Models",
                columns: table => new
                {
                    FridgeModelId = table.Column<Guid>(name: "Fridge_ModelId", type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridge_Models", x => x.FridgeModelId);
                });

            migrationBuilder.CreateTable(
                name: "Fridges",
                columns: table => new
                {
                    FridgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OwnerName = table.Column<string>(name: "Owner_Name", type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FridgeModelId = table.Column<Guid>(name: "Fridge_ModelId", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fridges", x => x.FridgeId);
                    table.ForeignKey(
                        name: "FK_Fridges_Fridge_Models_Fridge_ModelId",
                        column: x => x.FridgeModelId,
                        principalTable: "Fridge_Models",
                        principalColumn: "Fridge_ModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DefaultQuantity = table.Column<int>(name: "Default_Quantity", type: "int", nullable: false),
                    FridgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductsId);
                    table.ForeignKey(
                        name: "FK_Products_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalTable: "Fridges",
                        principalColumn: "FridgeId");
                });

            migrationBuilder.CreateTable(
                name: "FridgeProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    FridgeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FridgeProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FridgeProducts_Fridges_FridgeId",
                        column: x => x.FridgeId,
                        principalTable: "Fridges",
                        principalColumn: "FridgeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FridgeProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "ProductsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_FridgeId",
                table: "FridgeProducts",
                column: "FridgeId");

            migrationBuilder.CreateIndex(
                name: "IX_FridgeProducts_ProductsId",
                table: "FridgeProducts",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Fridges_Fridge_ModelId",
                table: "Fridges",
                column: "Fridge_ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FridgeId",
                table: "Products",
                column: "FridgeId");

            migrationBuilder.Sql("CREATE PROCEDURE UpdateDefaultValueIntoFridgeProducts\r\nAS\r\nBEGIN\r\n\tIF (EXISTS(\r\n\t\t\t\tSELECT * \r\n\t\t\t\tFROM FridgeProducts \r\n\t\t\t\tWHERE Quantity = 0\r\n\t\t\t\t))\r\n\tBEGIN\r\n\tUPDATE FridgeProducts \r\n\tSET Quantity = (\r\n\t\t\t\t\t\tSELECT Default_Quantity \r\n\t\t\t\t\t\tFROM Products\r\n\t\t\t\t\t\tWHERE ProductsId = FridgeProducts.ProductsId\r\n\t\t\t\t   )\r\n\tWHERE Id = FridgeProducts.Id AND Quantity = 0;\r\n\tEND;\r\nEND");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FridgeProducts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Fridges");

            migrationBuilder.DropTable(
                name: "Fridge_Models");

            migrationBuilder.Sql(@"DROP PROC UpdateDefaultValueIntoFridgeProducts");
        }
    }
}
