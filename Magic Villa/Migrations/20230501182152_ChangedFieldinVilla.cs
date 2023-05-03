using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_Villa.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFieldinVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 23, 51, 52, 606, DateTimeKind.Local).AddTicks(297));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 23, 51, 52, 606, DateTimeKind.Local).AddTicks(316));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Details", "ImageUrl" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 51, 52, 606, DateTimeKind.Local).AddTicks(318), "ssssss", "https://dotnetmastery.com/bluevillaimages/villa4.jpg" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Details", "ImageUrl" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 51, 52, 606, DateTimeKind.Local).AddTicks(321), "sssssddf", "https://dotnetmastery.com/bluevillaimages/villa5.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "Details", "ImageUrl" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1777), "https://dotnetmastery.com/bluevillaimages/villa4.jpg", "" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "Details", "ImageUrl" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 49, 27, 562, DateTimeKind.Local).AddTicks(1779), "https://dotnetmastery.com/bluevillaimages/villa5.jpg", "" });
        }
    }
}
