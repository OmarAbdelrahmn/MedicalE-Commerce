using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class addadmincontrol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Pharmacies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_AdminId",
                table: "Pharmacies",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_AspNetUsers_AdminId",
                table: "Pharmacies",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_AspNetUsers_AdminId",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_AdminId",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Pharmacies");

        }
    }
}
