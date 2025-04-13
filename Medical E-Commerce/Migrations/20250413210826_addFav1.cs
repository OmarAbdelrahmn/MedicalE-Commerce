using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class addFav1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Favourites",
                newName: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Items_ItemId",
                table: "Favourites",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Items_ItemId",
                table: "Favourites");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Favourites",
                newName: "Id");
        }
    }
}
