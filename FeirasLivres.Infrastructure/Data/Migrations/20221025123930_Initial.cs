using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeirasLivres.Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TS01_Distrito",
                columns: table => new
                {
                    TS01_id_Distrito = table.Column<Guid>(type: "TEXT", nullable: false),
                    cd_Distrito = table.Column<string>(type: "char", fixedLength: true, maxLength: 2, nullable: false),
                    no_Distrito = table.Column<string>(type: "varchar", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TS01_Distrito", x => x.TS01_id_Distrito);
                });

            migrationBuilder.CreateTable(
                name: "TS02_SubPrefeitura",
                columns: table => new
                {
                    TS02_id_SubPrefeitura = table.Column<Guid>(type: "TEXT", nullable: false),
                    cd_SubPrefeitura = table.Column<string>(type: "varchar", maxLength: 2, nullable: false),
                    no_SubPrefeitura = table.Column<string>(type: "varchar", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TS02_SubPrefeitura", x => x.TS02_id_SubPrefeitura);
                });

            migrationBuilder.CreateTable(
                name: "TP01_Feira",
                columns: table => new
                {
                    TP01_id_Feira = table.Column<Guid>(type: "TEXT", nullable: false),
                    no_Feira = table.Column<string>(type: "varchar", fixedLength: true, maxLength: 30, nullable: false),
                    nu_Registro = table.Column<string>(type: "char", fixedLength: true, maxLength: 6, nullable: false),
                    cd_SetorCensitarioIBGE = table.Column<string>(type: "char", fixedLength: true, maxLength: 15, nullable: false),
                    cd_AreaDePonderacaoIBGE = table.Column<string>(type: "char", fixedLength: true, maxLength: 13, nullable: false),
                    TS01_id_Distrito = table.Column<Guid>(type: "TEXT", nullable: false),
                    TS02_id_SubPrefeitura = table.Column<Guid>(type: "TEXT", nullable: false),
                    cd_Regiao5 = table.Column<int>(type: "INTEGER", nullable: false, comment: "1: Norte | 2: Leste | 3: Sul | 4: Oeste | 5: Centro"),
                    cd_Regiao8 = table.Column<int>(type: "INTEGER", nullable: false, comment: "11: Norte1 | 12: Norte2 | 21: Leste1 | 22: Leste2 | 31: Sul1 | 32: Sul2 | 4: Oeste | 5: Centro"),
                    tx_EnderecoLogradouro = table.Column<string>(type: "varchar", maxLength: 34, nullable: false),
                    tx_EnderecoNumero = table.Column<string>(type: "varchar", maxLength: 5, nullable: true),
                    tx_EnderecoBairro = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    tx_EnderecoReferencia = table.Column<string>(type: "varchar", maxLength: 24, nullable: true),
                    nu_Latitude = table.Column<double>(type: "REAL", nullable: false),
                    nu_Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TP01_Feira", x => x.TP01_id_Feira);
                    table.ForeignKey(
                        name: "FK_TP01_Feira_TS01_Distrito_TS01_id_Distrito",
                        column: x => x.TS01_id_Distrito,
                        principalTable: "TS01_Distrito",
                        principalColumn: "TS01_id_Distrito",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TP01_Feira_TS02_SubPrefeitura_TS02_id_SubPrefeitura",
                        column: x => x.TS02_id_SubPrefeitura,
                        principalTable: "TS02_SubPrefeitura",
                        principalColumn: "TS02_id_SubPrefeitura",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TP01_Feira_nu_Registro",
                table: "TP01_Feira",
                column: "nu_Registro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TP01_Feira_TS01_id_Distrito",
                table: "TP01_Feira",
                column: "TS01_id_Distrito");

            migrationBuilder.CreateIndex(
                name: "IX_TP01_Feira_TS02_id_SubPrefeitura",
                table: "TP01_Feira",
                column: "TS02_id_SubPrefeitura");

            migrationBuilder.CreateIndex(
                name: "IX_TS01_Distrito_cd_Distrito",
                table: "TS01_Distrito",
                column: "cd_Distrito",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TS02_SubPrefeitura_cd_SubPrefeitura",
                table: "TS02_SubPrefeitura",
                column: "cd_SubPrefeitura",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TP01_Feira");

            migrationBuilder.DropTable(
                name: "TS01_Distrito");

            migrationBuilder.DropTable(
                name: "TS02_SubPrefeitura");
        }
    }
}
