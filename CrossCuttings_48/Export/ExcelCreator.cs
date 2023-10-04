using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Cgpe.Du.CrossCuttings
{
    public class ExcelCreator
    {

        public bool GenerateExcel(DataSet ds, string name)
        {
            return GenerateExcel(ds, name, out _);
        }
        public bool GenerateExcel(DataSet ds, string name, out MemoryStream retf)
        {
            return GenerateExcel(ds.Tables[0], name, out retf);
        }
        public bool GenerateExcel(DataTable dt, string name, out MemoryStream retf)
        {

            using (var fs = new MemoryStream())
            {
                var excel = new XSSFWorkbook();
                var sheet = excel.CreateSheet(name);
                GenerateHeader(sheet, dt);
                GenerateRows(sheet, dt);
                excel.Write(fs);
                retf = fs;
            }

            return true;
        }


        private void GenerateHeader(ISheet sheet, DataTable dt)
        {
            var fila = sheet.CreateRow(1);
            ICell celda;
            for (var index = 0; index < dt.Columns.Count; index++)
            {
                celda = fila.CreateCell(index + 1);
                celda.SetCellValue(dt.Columns[index].ColumnName);
            }
        }

        private void GenerateRows(ISheet sheet, DataTable dt)
        {
            IRow row;
            ICell cell;
            for (var index = 0; index < dt.Rows.Count; index++)
            {
                row = sheet.CreateRow(index + 2);
                for (var column = 0; column < dt.Rows[index].ItemArray.Length; column++)
                {
                    cell = row.CreateCell(column + 1);
                    cell.SetCellValue(dt.Rows[index].ItemArray[column].ToString());
                }
            }
        }
    }
}
