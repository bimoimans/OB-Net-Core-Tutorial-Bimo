using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RumahMakanPadang.dal.Migrations
{
    public partial class added_chef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChefId",
                table: "Masakans",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Nama = table.Column<string>(maxLength: 100, nullable: true),
                    KTP = table.Column<string>(maxLength: 100, nullable: true),
                    Umur = table.Column<int>(nullable: false),
                    Spesialisasi = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masakans_Chefs_ChefId",
                table: "Masakans");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropIndex(
                name: "IX_Masakans_ChefId",
                table: "Masakans");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Masakans");
        }
    }
}
