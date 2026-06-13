using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VipshopWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarEntidadeGrupoDeProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grupo",
                table: "Produtos");

            migrationBuilder.AddColumn<Guid>(
                name: "grupo_id",
                table: "Produtos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "grupos_produtos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos_produtos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_grupo_id",
                table: "Produtos",
                column: "grupo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_grupos_produtos_grupo_id",
                table: "Produtos",
                column: "grupo_id",
                principalTable: "grupos_produtos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_grupos_produtos_grupo_id",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "grupos_produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_grupo_id",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "grupo_id",
                table: "Produtos");

            migrationBuilder.AddColumn<string>(
                name: "Grupo",
                table: "Produtos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
