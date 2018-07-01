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
            string appSettingFullName = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                , Constants.Application_Name, settingFileName);
            if (File.Exists(appSettingFullName))
                settingFileName = appSettingFullName;
            else
                File.Copy(settingFileName, appSettingFullName);

            if (!File.Exists(settingFileName))
            {
                MessageBox.Show($"找不到设置文件{settingFileName}");
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
            if (string.IsNullOrWhiteSpace(value))
                Log.Instance.Logger.ErrorFormat(
                    "{0}中没有找到设置{1}的值:", this.allSettingsContent, key);
            return value;
        }

        private int GetValueAsIntFromKey(string key)
        {
            string value = this.GetValueFromKey(key);
            int intValue = -1;
            if (!int.TryParse(value, out intValue))
                Log.Instance.Logger.ErrorFormat(
                    "设置值{0}={1}无法转换为整数", key, value);
            return intValue;
        }

        public string GetExcelFileName()
        {
            return this.GetValueFromKey(Constants.Settings_Key_ExcelFileName);
        }

        public int GetDisplayFontSize()
        {
            return this.GetValueAsIntFromKey(Constants.Settings_Key_DisplayFontSize);
        }

        public int GetDisplayRowsPerLoop()
        {
            return this.GetValueAsIntFromKey(Constants.Settings_Key_DisplayRowsPerLoop);
        }

        public TimeSpan GetDisplaySecondsPerLoop()
        {
            int value = this.GetValueAsIntFromKey(Constants.Settings_Key_DisplaySecondsPerLoop);
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
