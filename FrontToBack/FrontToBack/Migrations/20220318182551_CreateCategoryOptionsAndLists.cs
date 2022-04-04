using Microsoft.EntityFrameworkCore.Migrations;

namespace FrontToBack.Migrations
{
    public partial class CreateCategoryOptionsAndLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryOption_Categories_CategoryId",
                table: "CategoryOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryOption",
                table: "CategoryOption");

            migrationBuilder.RenameTable(
                name: "CategoryOption",
                newName: "CategoryOptions");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryOption_CategoryId",
                table: "CategoryOptions",
                newName: "IX_CategoryOptions_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryOptions",
                table: "CategoryOptions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CategoryOptionLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Option = table.Column<string>(nullable: true),
                    CategoryId = table.Column<string>(nullable: true),
                    CategoryId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOptionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryOptionLists_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOptionLists_CategoryId1",
                table: "CategoryOptionLists",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryOptions_Categories_CategoryId",
                table: "CategoryOptions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryOptions_Categories_CategoryId",
                table: "CategoryOptions");

            migrationBuilder.DropTable(
                name: "CategoryOptionLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryOptions",
                table: "CategoryOptions");

            migrationBuilder.RenameTable(
                name: "CategoryOptions",
                newName: "CategoryOption");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryOptions_CategoryId",
                table: "CategoryOption",
                newName: "IX_CategoryOption_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryOption",
                table: "CategoryOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryOption_Categories_CategoryId",
                table: "CategoryOption",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
