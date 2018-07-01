using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLoopDisplay
{
    class AppCfgsReader
    {
        public static string GetExcelFileName()
        {
            return ConfigurationManager.AppSettings["excelfilename"];
        }

        public static IList<int> GetDisplayColumnIndexes()
        {
            string displaycolumnindexes = ConfigurationManager.AppSettings["displaycolumnindexes"];
            return GetColumnsIndexesFromConfig(displaycolumnindexes);
        }

        private static IList<int> GetColumnsIndexesFromConfig(string displaycolumnindexes)
        {
            Regex reg = new Regex(@"\d+");
            List<int> cols = new List<int>();
            foreach (Match m in reg.Matches(displaycolumnindexes))
            {
                cols.Add(Convert.ToInt32(m.Groups[0].Value) - 1);
            }
            return cols;
        }
    }
}
