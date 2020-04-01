using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBudget.Data.Migrations
{
    public partial class Currency_field_added_to_Goal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyID",
                table: "Goals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_CurrencyID",
                table: "Goals",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Currencies_CurrencyID",
                table: "Goals",
                column: "CurrencyID",
                principalTable: "Currencies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Currencies_CurrencyID",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_CurrencyID",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "Goals");
        }
    }
}
