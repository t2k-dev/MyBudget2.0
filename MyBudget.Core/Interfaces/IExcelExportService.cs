using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IExcelExportService
    {
        /// <summary>
        /// Export transactions info to xlsx file.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ExcelSince">Period start date</param>
        /// <param name="ExcelTill">Period end date</param>
        public MemoryStream GetTransactionsListFile(string userID, DateTime? since, DateTime? till);
    }
}
