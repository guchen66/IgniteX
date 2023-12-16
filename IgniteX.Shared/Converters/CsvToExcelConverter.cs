using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Converters
{
    public class CsvToExcelConverter
    {
        public static void Show()
        {
            // 设置日志文件目录
            var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Warnings");

            // 获取最新的CSV文件
            var csvFilePath = Directory.GetFiles(logDirectory, $"*{DateTime.Now:yyyy-MM-dd}.csv")
                                        .OrderByDescending(f => new FileInfo(f).LastWriteTime)
                                        .FirstOrDefault();

            // 读取CSV文件数据
            var csvLines = new List<string>();
            if (!string.IsNullOrEmpty(csvFilePath))
            {
                using (var stream = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.None))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        csvLines.Add(line);
                    }
                }
            }

            // 创建新的Excel文件
            var excelFilePath = Path.Combine(logDirectory, $"{DateTime.Now:yyyyMMdd}.xls");
            using (var fs = new FileStream(excelFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var workbook = new HSSFWorkbook();
                var worksheet = workbook.CreateSheet("Logs");

                // 将CSV数据逐行写入Excel的单元格
                for (int i = 0; i < csvLines.Count; i++)
                {
                    var lineData = csvLines[i].Split(',');
                    IRow row = worksheet.CreateRow(i);
                    for (int j = 0; j < lineData.Length; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(lineData[j]);
                    }
                }

                // 保存Excel文件到磁盘
                workbook.Write(fs);
            }
        }

    }
}
