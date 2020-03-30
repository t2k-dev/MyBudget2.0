using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBudget.Data.Migrations
{
    public partial class Seeding_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "CreatedByID", "Icon", "IsSpendingCategory", "IsSystem", "Name" },
                values: new object[,]
                {
                    { 1, null, null, false, true, "Без категории" },
                    { 31, null, null, true, false, "Одежда" },
                    { 30, null, null, true, false, "Развлечения" },
                    { 29, null, null, true, false, "Клубы и бары" },
                    { 28, null, null, true, false, "Еда и продукты" },
                    { 27, null, null, true, false, "Спорт" },
                    { 26, null, null, true, false, "Транспорт" },
                    { 25, null, null, true, false, "Авто" },
                    { 24, null, null, true, false, "Жилье" },
                    { 23, null, null, true, false, "Здоровье" },
                    { 22, null, null, false, false, "Премия" },
                    { 21, null, null, false, false, "Бизнес" },
                    { 20, null, null, false, false, "Зарплата" },
                    { 8, null, null, true, true, "Пополнить цель" },
                    { 7, null, null, false, true, "Получить долг" },
                    { 6, null, null, true, true, "Отдать долг" },
                    { 5, null, null, false, true, "Остаток" },
                    { 4, null, null, true, true, "Дать в долг" },
                    { 3, null, null, false, true, "Взять в долг" },
                    { 2, null, null, true, true, "Без категории" },
                    { 32, null, null, true, false, "Связь" },
                    { 40, null, null, true, false, "Подарки" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 40);
        }
    }
}
