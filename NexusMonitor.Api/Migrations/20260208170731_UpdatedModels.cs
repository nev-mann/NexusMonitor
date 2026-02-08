using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "HighThreshold",
                table: "Devices",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "LowThreshold",
                table: "Devices",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "MeasurementsUnit",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Devices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                    table.ForeignKey(
                        name: "FK_Measurements_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    UserAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.UserAccountId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserAccountId",
                table: "Devices",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_DeviceId",
                table: "Measurements",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_UserAccounts_UserAccountId",
                table: "Devices",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "UserAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_UserAccounts_UserAccountId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserAccountId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "HighThreshold",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LowThreshold",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "MeasurementsUnit",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Devices");
        }
    }
}
