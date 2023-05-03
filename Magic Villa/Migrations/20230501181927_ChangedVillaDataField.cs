using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_Villa.Migrations
{
    /// <inheritdoc />
    public partial class ChangedVillaDataField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1756), "https://dotnetmastery.com/bluevillaimages/villa3.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1774), "https://dotnetmastery.com/bluevillaimages/villa1.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Details" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1777), "https://dotnetmastery.com/bluevillaimages/villa4.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Details" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1779), "https://dotnetmastery.com/bluevillaimages/villa5.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 27, 55, 389, DateTimeKind.Local).AddTicks(712), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 27, 55, 389, DateTimeKind.Local).AddTicks(725), "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Details" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 27, 55, 389, DateTimeKind.Local).AddTicks(728), "sm,adasas.as/.as/m.dmmas/,." });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Details" },
                values: new object[] { new DateTime(2023, 4, 30, 2, 27, 55, 389, DateTimeKind.Local).AddTicks(730), "sm,adasas.as/.as/m.das/,.gg" });
        }
    }
}
