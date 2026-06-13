using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VipshopWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarEstruturaItensEProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo_barras",
                table: "Produtos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "quantidade_estoque",
                table: "Produtos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ativo",
                table: "Itens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_cadastro",
                table: "Itens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_barras",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "quantidade_estoque",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ativo",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "data_cadastro",
                table: "Itens");
        }
    }
}
