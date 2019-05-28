using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.Infrastructure
{
    public class ExcelColumnInfo<T> where T : class
    {
        public int Order { get; set; }
        public string Header { get; set; }
        public Func<T, object> Accessor { get; set; }
    }

    public interface IExcelFileExporter
    {
        Task<byte[]> ExportForWeb<T>(string fileName, List<ExcelColumnInfo<T>> columnInfo, List<T> rows) where T : class;
    }
}
 