using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medical_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class Sedding_data_into_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtenstions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77B96C5D-F502-47TF-EE95-ABVN14A3CA22", "A7B75EE9-DB35-480D-9F9F-18D2E499B004", true, false, "Member", "MEMBER" },
                    { "77B96CED-F902-47EF-AE95-ABBE14A8CA22", "B0AD2D39-253B-42E4-88F2-F6FE83A614A8", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAddress", "UserFullName", "UserName" },
                values: new object[] { "59724D2D-E2B5-4C67-AB6F-D93478347B03", 0, "B4555410-F5B0-45B1-B963-1B2351A0723C", "admin@care-capsole.com", true, null, false, null, "ADMIN@CARE-CAPSOLE.COM", "ADMIN@CARE-CAPSOLE.COM", "AQAAAAIAAYagAAAAEMKTxZ9HaRYrU+qsV7mzmGP3I8zO9JhJ3Iqn7+xBa9UVleyF7aPCBsvLZqw/D/SXgw==", null, false, "9FABB58491024B7BB140E4D6658B5BDA", false, "lives in CareCapsole", "CareCapsole-Admin", "admin@care-capsole.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "77B96CED-F902-47EF-AE95-ABBE14A8CA22", "59724D2D-E2B5-4C67-AB6F-D93478347B03" });

            migrationBuilder.CreateIndex(
                name: "IX_Image_UserId",
                table: "Image",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77B96C5D-F502-47TF-EE95-ABVN14A3CA22");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "77B96CED-F902-47EF-AE95-ABBE14A8CA22", "59724D2D-E2B5-4C67-AB6F-D93478347B03" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77B96CED-F902-47EF-AE95-ABBE14A8CA22");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59724D2D-E2B5-4C67-AB6F-D93478347B03");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "AspNetUsers");
        }
    }
}
