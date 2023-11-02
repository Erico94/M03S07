using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FichaCadastro.Migrations
{
    /// <inheritdoc />
    public partial class ajustesColunaFicha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalhe_Ficha_FichaModelId",
                table: "Detalhe");

            migrationBuilder.RenameColumn(
                name: "FichaModelId",
                table: "Detalhe",
                newName: "FichaId");

            migrationBuilder.RenameIndex(
                name: "IX_Detalhe_FichaModelId",
                table: "Detalhe",
                newName: "IX_Detalhe_FichaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalhe_Ficha_FichaId",
                table: "Detalhe",
                column: "FichaId",
                principalTable: "Ficha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalhe_Ficha_FichaId",
                table: "Detalhe");

            migrationBuilder.RenameColumn(
                name: "FichaId",
                table: "Detalhe",
                newName: "FichaModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Detalhe_FichaId",
                table: "Detalhe",
                newName: "IX_Detalhe_FichaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalhe_Ficha_FichaModelId",
                table: "Detalhe",
                column: "FichaModelId",
                principalTable: "Ficha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
