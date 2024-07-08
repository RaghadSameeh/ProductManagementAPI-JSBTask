using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace productManagement.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_orders_OrderId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_orders_OrderId1",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_OrderId1",
                table: "products");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "products");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddForeignKey(
                name: "FK_products_orders_OrderId",
                table: "products",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_orders_OrderId",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "OrderId1",
                table: "products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_products_OrderId1",
                table: "products",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_products_orders_OrderId",
                table: "products",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_products_orders_OrderId1",
                table: "products",
                column: "OrderId1",
                principalTable: "orders",
                principalColumn: "OrderId");
        }
    }
}
