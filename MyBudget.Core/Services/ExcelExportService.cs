using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using MyBudget.Core.Interfaces;
using MyBudget.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyBudget.Core.Services
{
    public class ExcelExportService : IExcelExportService
    {
        #region ctor & fields
        private readonly ApplicationContext _context;

        public ExcelExportService(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        public MemoryStream GetTransactionsListFile(string userID, DateTime? since, DateTime? till)
        {
            if (since == null || till == null)
            {
                return null;
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Транзакции");

                //создадим заголовки у столбцов
                worksheet.Cell("A" + 1).Value = "Дата";
                worksheet.Cell("B" + 1).Value = "Наименование";
                worksheet.Cell("C" + 1).Value = "Категория";
                worksheet.Cell("D" + 1).Value = "Сумма";

                worksheet.Range("A1:D1").Style.Fill.BackgroundColor = XLColor.FromArgb(59, 89, 113);
                worksheet.Range("A1:D1").Style.Font.FontColor = XLColor.White;

                // Заполняем данными
                string UserGuid = userID;
                var transactions = _context.Transactions
                    .Where(transaction => transaction.UserID == userID
                        && transaction.TransactionDate >= since
                        && transaction.TransactionDate <= till)
                    .Include(transaction => transaction.Category)
                    .ToList();

                int k = 1;
                foreach (var t in transactions)
                {
                    k++;
                    worksheet.Cell("A" + k).Value = t.TransactionDate.ToShortDateString();
                    worksheet.Cell("B" + k).Value = t.Name;
                    if (t.Category != null)
                        worksheet.Cell("C" + k).Value = t.Category.Name;
                    if (t.IsSpending)
                    {
                        worksheet.Cell("D" + k).Value = -t.Amount;
                        worksheet.Cell("D" + k).Style.Font.FontColor = XLColor.FromArgb(245, 105, 93);
                    }
                    else
                    {
                        worksheet.Cell("D" + k).Value = t.Amount;
                        worksheet.Cell("D" + k).Style.Font.FontColor = XLColor.FromArgb(67, 172, 106);
                    }
                    worksheet.Cell("D" + k).Style.Font.Bold = true;
                }

                // пример создания сетки в диапазоне
                var rngTable = worksheet.Range("A2:D" + k);
                rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                worksheet.Columns().AdjustToContents(); //ширина столбца по содержимому

                // вернем пользователю файл без сохранения его на сервере
                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream;
                }
            };
        }
    }
}
