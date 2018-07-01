using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DataLoopDisplay
{
    class AppCfgsReader
    {
        private string allSettingsContent;
        public AppCfgsReader()
        {
            string settingFileName = "settings.txt";
            if (!File.Exists(settingFileName))
            {
                MessageBox.Show($"找不到文件{settingFileName}");
                return;
            }
            try
            {
                this.allSettingsContent = File.ReadAllText(settingFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetValueFromKey(string key)
        {
            Regex reg = new Regex(key + ":(.+)");
            string value = reg.Match(this.allSettingsContent).Groups[1].Value.Trim();
            return value;
        }

        public string GetExcelFileName()
        {
            //return ConfigurationManager.AppSettings["Excel文件名"];
            return this.GetValueFromKey("Excel文件名");
        }

        public int GetDisplayRowsPerLoop()
        {
            //string value = ConfigurationManager.AppSettings["每次显示行数"];
            string value = this.GetValueFromKey("每次显示行数");
            return Convert.ToInt32(value);
        }

        public TimeSpan GetDisplaySecondsPerLoop()
        {
            //string value = ConfigurationManager.AppSettings["每次显示秒数"];
            string value = this.GetValueFromKey("每次显示秒数");
            return TimeSpan.FromSeconds(Convert.ToInt32(value));
        }

        public IList<int> GetDisplayColumnIndexes()
        {
            //string displaycolumnindexes = ConfigurationManager.AppSettings["显示列下标"];
            string displaycolumnindexes = this.GetValueFromKey("显示列下标");
            return GetColumnsIndexesFromConfig(displaycolumnindexes);
        }

        private IList<int> GetColumnsIndexesFromConfig(string displaycolumnindexes)
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
