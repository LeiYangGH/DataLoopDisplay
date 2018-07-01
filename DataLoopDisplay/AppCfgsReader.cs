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
            return ConfigurationManager.AppSettings["Excel文件名"];
        }

        public static int GetDisplayRowsPerLoop()
        {
            string value = ConfigurationManager.AppSettings["每次显示行数"];
            return Convert.ToInt32(value);
        }

        public static TimeSpan GetDisplaySecondsPerLoop()
        {
            string value = ConfigurationManager.AppSettings["每次显示秒数"];
            return TimeSpan.FromSeconds(Convert.ToInt32(value));
        }

        public static IList<int> GetDisplayColumnIndexes()
        {
            string displaycolumnindexes = ConfigurationManager.AppSettings["显示列下标"];
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
