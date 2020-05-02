using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IExcelExportService
    {
        public MemoryStream GetTransactionsListFile(string userID, DateTime? ExcelSince, DateTime? ExcelTill);
    }
}
