using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FridgeProducts.Migrations
{
    /// <inheritdoc />
    public partial class ScoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE PROCEDURE UpdateDefaultValueIntoFridgeProducts\r\nAS\r\nBEGIN\r\n\tIF (EXISTS(\r\n\t\t\t\tSELECT * \r\n\t\t\t\tFROM FridgeProducts \r\n\t\t\t\tWHERE Quantity = 0\r\n\t\t\t\t))\r\n\tBEGIN\r\n\tUPDATE FridgeProducts \r\n\tSET Quantity = (\r\n\t\t\t\t\t\tSELECT DefaultQuantity \r\n\t\t\t\t\t\tFROM Products\r\n\t\t\t\t\t\tWHERE ProductId = FridgeProducts.ProductId\r\n\t\t\t\t   )\r\n\tWHERE Id = FridgeProducts.Id AND Quantity = 0;\r\n\tEND;\r\nEND");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
