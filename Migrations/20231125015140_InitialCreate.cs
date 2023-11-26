using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVeiculos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Grupo = table.Column<int>(type: "INTEGER", nullable: false),
                    Unidade = table.Column<string>(type: "TEXT", nullable: false),
                    PrCusto = table.Column<decimal>(type: "TEXT", nullable: false),
                    Margem = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrVenda = table.Column<decimal>(type: "TEXT", nullable: false),
                    Ncm = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<string>(type: "TEXT", nullable: false),
                    CFOP = table.Column<string>(type: "TEXT", nullable: false),
                    CEST = table.Column<string>(type: "TEXT", nullable: false),
                    PrVendaPrazo = table.Column<string>(type: "TEXT", nullable: false),
                    DtCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ViaReciboVeiculo = table.Column<int>(type: "INTEGER", nullable: false),
                    RenavamVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    RegistroVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    RntrcVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    ExerEmisDocVeiculo = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    ChassiVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    CombustivelVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    FabricacaoVeiculo = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloVeiculo = table.Column<int>(type: "INTEGER", nullable: false),
                    CorVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    CategoriaVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    EmissaoDocumentoVeiculo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LocalRegistroVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    CapPotCilVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    MarcaModeloVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    EspecieVeiculo = table.Column<string>(type: "TEXT", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VeiculoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeArquivo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fotos_Veiculo_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_VeiculoId",
                table: "Fotos",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
