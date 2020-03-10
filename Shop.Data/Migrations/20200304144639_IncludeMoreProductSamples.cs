using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Data.Migrations
{
    public partial class IncludeMoreProductSamples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImagePath", "Name", "Price" },
                values: new object[,]
                {
                    { 10, "sample_10.jpg", "Drainpipe", 29m },
                    { 11, "sample_11.jpg", "Window", 400m },
                    { 12, "sample_12.jpg", "Coal", 22.99m },
                    { 13, "sample_13.jpg", "Plywood", 7.50m },
                    { 14, "sample_14.jpg", "Brick", 3.99m },
                    { 15, "sample_15.jpg", "Cement mixer", 400.0m },
                    { 16, "sample_16.jpg", "Barrow", 199.99m },
                    { 17, "sample_17.jpg", "Sink", 169.99m },
                    { 18, "sample_18.jpg", "Board", 35.0m },
                    { 19, "sample_19.jpg", "Lamp", 79.99m },
                    { 20, "sample_20.jpg", "LED Strip", 30.99m },
                    { 21, "sample_21.jpg", "Paper bag", 0.99m },
                    { 22, "sample_22.jpg", "Saw", 39.99m },
                    { 23, "sample_23.jpg", "Knife", 19.99m },
                    { 24, "sample_24.jpg", "Scissors", 9.99m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);
        }
    }
}
