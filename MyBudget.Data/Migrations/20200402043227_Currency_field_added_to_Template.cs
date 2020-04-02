using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBudget.Data.Migrations
{
    public partial class Currency_field_added_to_Template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "Templates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Templates_CurrencyID",
                table: "Templates",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Currencies_CurrencyID",
                table: "Templates",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Currencies_CurrencyID",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_CurrencyID",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "Templates");
        }
    }
}
