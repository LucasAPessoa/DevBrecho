using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DevBrecho.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedoras",
                columns: table => new
                {
                    FornecedoraId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Telefone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedoras", x => x.FornecedoraId);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    SetorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.SetorId);
                });

            migrationBuilder.CreateTable(
                name: "Bolsas",
                columns: table => new
                {
                    BolsaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataDeEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataMensagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FornecedoraId = table.Column<int>(type: "integer", nullable: false),
                    SetorId = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeDePecasSemCadastro = table.Column<int>(type: "integer", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolsas", x => x.BolsaId);
                    table.ForeignKey(
                        name: "FK_Bolsas_Fornecedoras_FornecedoraId",
                        column: x => x.FornecedoraId,
                        principalTable: "Fornecedoras",
                        principalColumn: "FornecedoraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bolsas_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "SetorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PecasCadastradas",
                columns: table => new
                {
                    PecaCadastradaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoDaPeca = table.Column<string>(type: "text", nullable: true),
                    BolsaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PecasCadastradas", x => x.PecaCadastradaId);
                    table.ForeignKey(
                        name: "FK_PecasCadastradas_Bolsas_BolsaId",
                        column: x => x.BolsaId,
                        principalTable: "Bolsas",
                        principalColumn: "BolsaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_FornecedoraId",
                table: "Bolsas",
                column: "FornecedoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolsas_SetorId",
                table: "Bolsas",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_PecasCadastradas_BolsaId",
                table: "PecasCadastradas",
                column: "BolsaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PecasCadastradas");

            migrationBuilder.DropTable(
                name: "Bolsas");

            migrationBuilder.DropTable(
                name: "Fornecedoras");

            migrationBuilder.DropTable(
                name: "Setores");
        }
    }
}
