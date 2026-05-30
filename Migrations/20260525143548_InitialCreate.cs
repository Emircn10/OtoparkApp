using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoparkApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Araclar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plaka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AracTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SahibiAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SahibiTelefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Araclar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarifeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracTipi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IlkSaatUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrtaSaatUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UzunSaatUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GirisKayitlari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracId = table.Column<int>(type: "int", nullable: false),
                    GirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToplamSureDakika = table.Column<int>(type: "int", nullable: true),
                    OdenenUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OdemeDurumu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GirisKayitlari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GirisKayitlari_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GirisKayitlari_AracId",
                table: "GirisKayitlari",
                column: "AracId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GirisKayitlari");

            migrationBuilder.DropTable(
                name: "Tarifeler");

            migrationBuilder.DropTable(
                name: "Araclar");
        }
    }
}
