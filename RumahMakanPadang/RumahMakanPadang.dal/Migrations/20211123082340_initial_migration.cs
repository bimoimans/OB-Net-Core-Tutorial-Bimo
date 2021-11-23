using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RumahMakanPadang.dal.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Masakans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Nama = table.Column<string>(maxLength: 100, nullable: true),
                    Harga = table.Column<int>(nullable: false),
                    Deskripsi = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masakans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masakans_Nama",
                table: "Masakans",
                column: "Nama",
                unique: true,
                filter: "[Nama] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Masakans");
        }
    }
}
