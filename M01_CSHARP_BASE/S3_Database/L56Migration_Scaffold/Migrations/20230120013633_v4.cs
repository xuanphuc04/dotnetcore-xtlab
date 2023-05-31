using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L56MigrationScaffold.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagIdd",
                table: "ArticleTags");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTags_TagIdd",
                table: "ArticleTags");

            migrationBuilder.DropColumn(
                name: "TagIdd",
                table: "ArticleTags");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTags_Tags_TagId",
                table: "ArticleTags");

            migrationBuilder.AddColumn<int>(
                name: "TagIdd",
                table: "ArticleTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTags_TagIdd",
                table: "ArticleTags",
                column: "TagIdd");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTags_Tags_TagIdd",
                table: "ArticleTags",
                column: "TagIdd",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
