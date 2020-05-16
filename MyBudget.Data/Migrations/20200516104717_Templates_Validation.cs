using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBudget.Data.Migrations
{
    public partial class Templates_Validation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Templates",
                maxLength: 90,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Templates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 90,
                oldNullable: true);
        }
    }
}
