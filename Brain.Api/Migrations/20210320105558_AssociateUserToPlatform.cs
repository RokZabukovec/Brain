using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brain.Api.Migrations
{
    public partial class AssociateUserToPlatform : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Platforms",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_UserId",
                table: "Platforms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Platforms_AspNetUsers_UserId",
                table: "Platforms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platforms_AspNetUsers_UserId",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_UserId",
                table: "Platforms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Platforms");
        }
    }
}
