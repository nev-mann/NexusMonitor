using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class DevicesNameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Devicess",
                table: "Devicess");

            migrationBuilder.RenameTable(
                name: "Devicess",
                newName: "Devices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Devicess");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devicess",
                table: "Devicess",
                column: "DeviceId");
        }
    }
}
