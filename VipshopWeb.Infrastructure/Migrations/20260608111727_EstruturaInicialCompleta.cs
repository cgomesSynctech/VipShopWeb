using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VipshopWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaInicialCompleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Custo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_favorecidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    descricao = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_favorecidos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    senha_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UnidadeVenda = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Grupo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Referencia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Itens_Id",
                        column: x => x.Id,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeMaoObra = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TempoOcupacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicos_Itens_Id",
                        column: x => x.Id,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "favorecidos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_favorecido_id = table.Column<int>(type: "integer", nullable: false),
                    nome_razao_social = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    nome_fantasia_apelido = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    tipo_pessoa = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorecidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_favorecidos_tipos_favorecidos_tipo_favorecido_id",
                        column: x => x.tipo_favorecido_id,
                        principalTable: "tipos_favorecidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    favorecido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    limite_credito = table.Column<decimal>(type: "numeric", nullable: false),
                    observacao = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.favorecido_id);
                    table.ForeignKey(
                        name: "FK_clientes_favorecidos_favorecido_id",
                        column: x => x.favorecido_id,
                        principalTable: "favorecidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    favorecido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    inscricao_estadual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    prazo_entrega_estimado_dias = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.favorecido_id);
                    table.ForeignKey(
                        name: "FK_fornecedores_favorecidos_favorecido_id",
                        column: x => x.favorecido_id,
                        principalTable: "favorecidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    favorecido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ctps = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    salario = table.Column<decimal>(type: "numeric", nullable: false),
                    data_admissao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cargo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.favorecido_id);
                    table.ForeignKey(
                        name: "FK_funcionarios_favorecidos_favorecido_id",
                        column: x => x.favorecido_id,
                        principalTable: "favorecidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tipos_favorecidos",
                columns: new[] { "id", "descricao" },
                values: new object[,]
                {
                    { 1, "Cliente" },
                    { 2, "Fornecedor" },
                    { 3, "Funcionário" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "ativo", "login", "nome", "senha_hash" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), true, "admin", "Administrador Vipshop", "123" });

            migrationBuilder.CreateIndex(
                name: "IX_favorecidos_tipo_favorecido_id",
                table: "favorecidos",
                column: "tipo_favorecido_id");

            migrationBuilder.CreateIndex(
                name: "IX_Itens_Codigo",
                table: "Itens",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "fornecedores");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "favorecidos");

            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "tipos_favorecidos");
        }
    }
}
