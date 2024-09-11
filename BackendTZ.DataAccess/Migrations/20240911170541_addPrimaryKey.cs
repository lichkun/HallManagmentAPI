using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendTZ.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Services",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Halls",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "BaseRate", "Capacity", "Name" },
                values: new object[,]
                {
                    { new Guid("33b9677f-d265-4233-80ad-424d004c60ce"), 3500m, 100, "Зал B" },
                    { new Guid("754790f9-7655-4548-b9ef-329503293043"), 2000m, 50, "Зал А" },
                    { new Guid("c8889cfe-e315-44f9-8924-e13633382b15"), 1500m, 30, "Зал C" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("24970e38-c963-4612-90f7-404a931ac66e"), "Проєктор", 500m },
                    { new Guid("3c506468-23bb-43a9-9291-5b01b03b0613"), "Звук", 700m },
                    { new Guid("b37ddcb1-325b-4a0e-b667-051be081b4c3"), "Wi-Fi", 300m }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_ConferenceHall_Capacity",
                table: "Halls",
                sql: "[Capacity]<= 1250");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ConferenceHall_Capacity",
                table: "Halls");

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: new Guid("33b9677f-d265-4233-80ad-424d004c60ce"));

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: new Guid("754790f9-7655-4548-b9ef-329503293043"));

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: new Guid("c8889cfe-e315-44f9-8924-e13633382b15"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("24970e38-c963-4612-90f7-404a931ac66e"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("3c506468-23bb-43a9-9291-5b01b03b0613"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("b37ddcb1-325b-4a0e-b667-051be081b4c3"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
