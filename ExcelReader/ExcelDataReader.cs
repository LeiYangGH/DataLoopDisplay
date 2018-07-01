using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
namespace ExcelReader
{
    public class ExcelDataReader
    {
        public static DataTable ReadToDataTable(string fileName)
        {

            DataTable dt = new DataTable();
            string connStr =string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"",fileName);
            string sql = "Select * From [Sheet1$]";
            using (OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }

}
