using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class intoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_AdminId",
                table: "Pharmacies");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_AdminId",
                table: "Pharmacies",
                column: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_AdminId",
                table: "Pharmacies");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_AdminId",
                table: "Pharmacies",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");
        }
    }
}
