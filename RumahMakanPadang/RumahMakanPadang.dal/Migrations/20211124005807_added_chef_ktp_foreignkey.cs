using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RumahMakanPadang.dal.Migrations
{
    public partial class added_chef_ktp_foreignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masakans_Chefs_ChefId",
                table: "Masakans");

            migrationBuilder.DropIndex(
                name: "IX_Masakans_ChefId",
                table: "Masakans");

            migrationBuilder.DropIndex(
                name: "IX_Chefs_KTP",
                table: "Chefs");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Masakans");

            migrationBuilder.AddColumn<string>(
                name: "ChefKTP",
                table: "Masakans",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KTP",
                table: "Chefs",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Chefs_KTP",
                table: "Chefs",
                column: "KTP");

            migrationBuilder.CreateIndex(
                name: "IX_Masakans_ChefKTP",
                table: "Masakans",
                column: "ChefKTP");

            migrationBuilder.CreateIndex(
                name: "IX_Chefs_KTP",
                table: "Chefs",
                column: "KTP",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Masakans_Chefs_ChefKTP",
                table: "Masakans",
                column: "ChefKTP",
                principalTable: "Chefs",
                principalColumn: "KTP",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masakans_Chefs_ChefKTP",
                table: "Masakans");

            migrationBuilder.DropIndex(
                name: "IX_Masakans_ChefKTP",
                table: "Masakans");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Chefs_KTP",
                table: "Chefs");

            migrationBuilder.DropIndex(
                name: "IX_Chefs_KTP",
                table: "Chefs");

            migrationBuilder.DropColumn(
                name: "ChefKTP",
                table: "Masakans");

            migrationBuilder.AddColumn<Guid>(
                name: "ChefId",
                table: "Masakans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KTP",
                table: "Chefs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 16);

            migrationBuilder.CreateIndex(
                name: "IX_Masakans_ChefId",
                table: "Masakans",
                column: "ChefId");

            migrationBuilder.CreateIndex(
                name: "IX_Chefs_KTP",
                table: "Chefs",
                column: "KTP",
                unique: true,
                filter: "[KTP] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Masakans_Chefs_ChefId",
                table: "Masakans",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
