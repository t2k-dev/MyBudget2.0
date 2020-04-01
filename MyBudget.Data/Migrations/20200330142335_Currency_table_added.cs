using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyBudget.Data.Migrations
{
    public partial class Currency_table_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultCurrency",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultCurrencyID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Symbol = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "ID", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, "Тенге", "₸" },
                    { 2, "Доллар США", "$" },
                    { 3, "Евро", "€" },
                    { 4, "Российский рубль", "₽" },
                    { 5, "Фунт стерлингов", "£" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyID",
                table: "Transactions",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultCurrencyID",
                table: "AspNetUsers",
                column: "DefaultCurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Currencies_DefaultCurrencyID",
                table: "AspNetUsers",
                column: "DefaultCurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Currencies_CurrencyID",
                table: "Transactions",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Currencies_DefaultCurrencyID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Currencies_CurrencyID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CurrencyID",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DefaultCurrencyID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DefaultCurrencyID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "DefaultCurrency",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
