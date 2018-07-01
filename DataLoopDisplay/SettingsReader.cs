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
    class SettingsReader
    {
        private string allSettingsContent;
        public SettingsReader()
        {
            string settingFileName = Constants.Settings_FileName;
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
            return this.GetValueFromKey(Constants.Settings_Key_ExcelFileName);
        }

        public int GetDisplayRowsPerLoop()
        {
            string value = this.GetValueFromKey(Constants.Settings_Key_DisplayRowsPerLoop);
            return Convert.ToInt32(value);
        }

        public TimeSpan GetDisplaySecondsPerLoop()
        {
            string value = this.GetValueFromKey(Constants.Settings_Key_DisplaySecondsPerLoop);
            return TimeSpan.FromSeconds(Convert.ToInt32(value));
        }

        public IList<int> GetDisplayColumnIndexes()
        {
            string displaycolumnindexes = this.GetValueFromKey(Constants.Settings_Key_DisplayColumnIndexes);
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
