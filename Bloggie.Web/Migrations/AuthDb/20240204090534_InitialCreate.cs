using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf355ec2-6177-45b2-a733-8f38793ecf36",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3c24f3f-be96-4c1e-9f15-3642648c6cae", "AQAAAAIAAYagAAAAED+Gid8VQ7sr9A9G5rRFmEHoTBzdTAeMFdJfoGBTA6kGlyZT+OdNiScJMwRfyZWVIQ==", "e25b3401-7423-40fd-9867-d4f0d4de0e07" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf355ec2-6177-45b2-a733-8f38793ecf36",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a9b2be4-8fd9-43c6-b92b-483fbc1d4c5d", "AQAAAAIAAYagAAAAEMMW08O/s4HyM1HRRS7KSpKA+DEb17dejq5RlFMslqMNUY/C8HeZke8iI8Xec2eGhg==", "dfad9d76-e63e-4888-b043-38468eefa965" });
        }
    }
}
