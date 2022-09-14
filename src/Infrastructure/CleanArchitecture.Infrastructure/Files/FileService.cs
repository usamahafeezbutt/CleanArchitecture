
using CleanArchitecture.Application.Common.Constants.Files;
using CleanArchitecture.Application.Common.Interfaces.Files;
using Microsoft.VisualBasic;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace CleanArchitecture.Infrastructure.Files
{
    public class FileService : IFileService
    {
        public async Task<MemoryStream> ToCSV<T>(List<T> data)
        => await Task.FromResult(new MemoryStream(Encoding.UTF8.GetBytes(string.Join(",", data.Select(x => x!.ToString()).ToArray()) ?? "")));

        public Task<MemoryStream> ToExcel<T>(List<T> data)
        {
            var dataTable = ToDataTable(data);

            if (dataTable == null || dataTable.Rows.Count < 1)
            {
                return null!;
            }

            IWorkbook workbook = PrepareWorkbook( dataTable);

            using var exportData = new MemoryStream();
            workbook.Write(exportData);
            return Task.FromResult(exportData);
        }

        private static IWorkbook PrepareWorkbook(DataTable dataTable)
        {
            var rowIndex = 0;

            IWorkbook workbook = new XSSFWorkbook();

            ICellStyle style = PrepareWorkbookStyle(workbook);

            var sheet1 = workbook.CreateSheet("Sheet 1");

            var row1 = sheet1.CreateRow(rowIndex++);

            PrepareColumns(dataTable, style, sheet1, row1);

            PrepareRows(dataTable, rowIndex, sheet1);

            return workbook;
        }

        private static void PrepareRows(DataTable dataTable, int rowIndex, ISheet sheet1)
        {
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = sheet1.CreateRow(rowIndex + 1);
                for (var j = 0; j < dataTable.Columns.Count; j++)
                {
                    var columnName = dataTable.Columns[j].ToString();

                    var cell = row.CreateCell(j, CellType.String);
                    cell.SetCellValue(dataTable.Rows[i][columnName].ToString());


                    sheet1.AutoSizeColumn(cell.ColumnIndex);
                }
                rowIndex++;
            }
        }

        private static void PrepareColumns(DataTable dataTable, ICellStyle style, ISheet sheet1, IRow row1)
        {
            for (var k = 0; k < dataTable.Columns.Count; k++)
            {
                var cell = row1.CreateCell(k);
                cell.SetCellValue(dataTable.Columns[k].ColumnName);
                cell.CellStyle = style;

                var mergeCell = new CellRangeAddress(0, 1, k, k);
                sheet1.AddMergedRegion(mergeCell);
                sheet1.AutoSizeColumn(cell.ColumnIndex);

            }
        }

        private static ICellStyle PrepareWorkbookStyle(IWorkbook workbook)
        {
            var xFont = (XSSFFont)workbook.CreateFont();

            xFont.FontHeightInPoints = ExcelConstants.FontHeightInPoints;
            xFont.FontName = ExcelConstants.FontName;
            xFont.IsBold = true;

            var xStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            xStyle.SetFont(xFont);
            xStyle.Alignment = HorizontalAlignment.Center;
            ICellStyle style = xStyle;
            return style;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);

                if (type != null) dataTable.Columns.Add(prop.Name, type);
            }
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null)!;
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}
