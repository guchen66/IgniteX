using Bogus.DataSets;
using IgniteX.Shared.Models.RepiceData;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SqlSugarDB.Models;
using SqlSugarDB.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace IgniteX.Shared.Extensions
{
    public static class ExcelExtension
    {
        static IgniteTubeService db1 = new IgniteTubeService();
        static DeviceStateService db2 = new DeviceStateService();
        static RepiceInfoService db3 = new RepiceInfoService();

        /// <summary>
        /// 点火管表单下载
        /// </summary>
        /// <param name="name"></param>
        public async static Task<bool> WriteIgniteDataToExcel(string name)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string result = dateTime.ToString("yyyyMMdd");
                List<IgniteTubeInfo> lists = await db1.QueryListAsync();
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = Path.Combine(desktopPath, $"{result}-{name}.xlsx");

                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");

                // 写入表头
                List<string> headString = new List<string>()
                {
                  "工单号","产品名称","颜色","极性","绝缘电阻","桥路电阻","热瞬态",
                };

                IRow row = sheet.CreateRow(0);       //创建第一行
                for (int i = 0; i < headString.Count; ++i)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(headString[i]);
                }
                // 写入数据
                int rowIndex = 1;
                foreach (IgniteTubeInfo ignite in lists)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(ignite.OrderNumber);
                    dataRow.CreateCell(1).SetCellValue(ignite.IgniteName);
                  //  dataRow.CreateCell(2).SetCellValue(ignite.Color);
                 //   dataRow.CreateCell(3).SetCellValue(ignite.Polarity);
                    dataRow.CreateCell(4).SetCellValue(ignite.InsulResistive.ToString());
                    dataRow.CreateCell(5).SetCellValue(ignite.BridgeResistive.ToString());
                    dataRow.CreateCell(6).SetCellValue(ignite.ThermalInitState.ToString());
                    rowIndex++;
                }
                // 保存文件
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    workbook.Write(fs);
                }
               // throw new Exception("模拟下载失败");
            }
            catch
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// 设备状态表单下载
        /// </summary>
        /// <param name="name"></param>
        public async static Task<bool> WriteDeviceStateDataToExcel(string name)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string result = dateTime.ToString("yyyyMMdd");
                List<DeviceStatus> lists = await db2.QueryListAsync();
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = Path.Combine(desktopPath, $"{result}-{name}.xlsx");

                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");

                // 写入表头
                List<string> headString = new List<string>()
               {
               "主键Id", "工单号","运行时间","停止时间","故障时间"
               };

                IRow row = sheet.CreateRow(0);       //创建第一行
                for (int i = 0; i < headString.Count; ++i)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(headString[i]);
                }
                // 写入数据
                int rowIndex = 1;
                foreach (DeviceStatus ignite in lists)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(ignite.Id);
                   // dataRow.CreateCell(1).SetCellValue(ignite.OrderNumber);
                  /*  dataRow.CreateCell(2).SetCellValue(ignite.RunTime.ToString());
                    dataRow.CreateCell(3).SetCellValue(ignite.StopTime.ToString());
                    dataRow.CreateCell(4).SetCellValue(ignite.FaultTime.ToString());*/
                    rowIndex++;
                }
                // 保存文件
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    workbook.Write(fs);
                }
            }
            catch 
            {
                return false;
            }
          
            return true;
        }



        /// <summary>
        /// 配方表单下载
        /// </summary>
        /// <param name="name"></param>
        public async static Task<bool> WriteRepiceListToExcel(string name)
        {
            try
            {
                DateTime dateTime = DateTime.Today;
                string result = dateTime.ToString("yyyyMMdd");
                List<RepiceInfo> lists= await db3.QueryListAsync();
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = Path.Combine(desktopPath, $"{result}-{name}.xlsx");

                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");

               
                List<string> headString = new List<string>()
                {
                 "电子元件", "测得值","上限值","下限值",
                };

                IRow row = sheet.CreateRow(0);       //创建第一行
                for (int i = 0; i < headString.Count; ++i)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(headString[i]);
                }
              
                int rowIndex = 1;
                foreach (RepiceInfo ignite in lists)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    dataRow.CreateCell(0).SetCellValue(ignite.Id);
                   /* dataRow.CreateCell(1).SetCellValue(ignite.AvgResult);
                    dataRow.CreateCell(2).SetCellValue(ignite.MaxResult);
                    dataRow.CreateCell(3).SetCellValue(ignite.MixResult);*/
                    rowIndex++;
                }
               
                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    workbook.Write(fs);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }


        public static void GenerateColumns<T>(DataGrid dataGrid, List<string> headerList, List<T> dataList)
        {
            // 清空现有的列
            dataGrid.Columns.Clear();
            // 设置 AutoGenerateColumns 属性为 true
            dataGrid.AutoGenerateColumns = true;
            // 创建并添加列
            foreach (string header in headerList)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = header;
                dataGrid.Columns.Add(column);
            }

            // 绑定数据源
            dataGrid.ItemsSource = dataList;
        }

    }
}
