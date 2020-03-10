using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.Migrations
{
    public partial class SeedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImagePath", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "sample_1.jpg", "Ladder", 125.0m },
                    { 2, "sample_2.jpg", "Drill", 150m },
                    { 3, "sample_3.jpg", "Grinder", 150m },
                    { 4, "sample_4.jpg", "Gloves", 3.5m },
                    { 5, "sample_5.jpg", "Thermometer", 19.99m },
                    { 6, "sample_6.jpg", "Measure tape", 3.5m },
                    { 7, "sample_7.jpg", "WD40", 7m },
                    { 8, "sample_8.jpg", "Screwdrivers", 12.99m },
                    { 9, "sample_9.jpg", "Lawnmower", 199.99m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
