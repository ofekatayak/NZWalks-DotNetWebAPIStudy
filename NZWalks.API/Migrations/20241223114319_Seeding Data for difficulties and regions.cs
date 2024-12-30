using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0ff1715e-907f-416c-bf40-d73576e12601"), "Medium" },
                    { new Guid("54f977ba-098c-4188-aa43-c1ad2126d0b7"), "Easy" },
                    { new Guid("6419a4a1-3252-4e16-a84e-6c8b1647985b"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("07ddab4e-a3b3-4743-92e7-c3f750d17b26"), "AKL", "Auckland", null },
                    { new Guid("1c0940ab-79b3-4599-a77f-ca35ee17b7e2"), "AKL", "Aucland", null },
                    { new Guid("1f6f4b7b-086b-4f16-811d-e45c6ed00be1"), "NYC", "New York City", null },
                    { new Guid("a98d01aa-fd0f-4a0b-8823-f78516394cc9"), "PTL", "Portland", null },
                    { new Guid("c6d37314-715d-45c2-9dd7-6c1cc5d3bc3e"), "MIA", "Miami", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0ff1715e-907f-416c-bf40-d73576e12601"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("54f977ba-098c-4188-aa43-c1ad2126d0b7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6419a4a1-3252-4e16-a84e-6c8b1647985b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("07ddab4e-a3b3-4743-92e7-c3f750d17b26"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1c0940ab-79b3-4599-a77f-ca35ee17b7e2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1f6f4b7b-086b-4f16-811d-e45c6ed00be1"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a98d01aa-fd0f-4a0b-8823-f78516394cc9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c6d37314-715d-45c2-9dd7-6c1cc5d3bc3e"));
        }
    }
}
