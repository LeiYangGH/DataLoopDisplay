using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace ExcelReader
{
    public class ExcelDataReader
    {
        public static DataTable ReadToDataTable(string fileName)
        {
            DataTable dt = new DataTable();
            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/zxm/DataDisplay/测试 Microsoft Excel 工作表.xlsx;Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            string sql = "Select * From [Sheet1$]";
            using (OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }
}
