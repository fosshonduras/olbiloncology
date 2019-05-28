using OfficeOpenXml;
using OLBIL.OncologyApplication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyCrossCutting
{
    public class ExcelFileExporter : IExcelFileExporter
    {
        public async Task<byte[]> ExportForWeb<T>(string fileName, List<ExcelColumnInfo<T>> columnInfoList, List<T> rows) where T : class
        {
            byte[] fileContents;

            using(var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");
                var columnIndex = 1;
                foreach (var columnInfo in columnInfoList.OrderBy(c => c.Order))
                {
                    worksheet.Cells[1, columnIndex].Value = columnInfo.Header;
                    columnIndex++;
                }
                columnIndex = 1;

                foreach (var columnInfo in columnInfoList.OrderBy(c => c.Order))
                {
                    var rowIndex = 2;
                    foreach (var row in rows)
                    {
                        var cellValue = columnInfo.Accessor(row);
                        worksheet.Cells[rowIndex, columnIndex].Value = cellValue;
                        rowIndex++;
                    }
                    
                    columnIndex++;
                }
                fileContents = package.GetAsByteArray();
            }

            return fileContents;
        }
    }
}
