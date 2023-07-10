using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTblCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryid = table.Column<int>(name: "category_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_category_id",
                table: "Product",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_category_id",
                table: "Product",
                column: "category_id",
                principalTable: "Category",
                principalColumn: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_category_id",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Product_category_id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "category_id",
                table: "Product");
        }
    }
}
