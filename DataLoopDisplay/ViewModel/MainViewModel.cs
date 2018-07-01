using GalaSoft.MvvmLight;
using System.Data;
using ExcelReader;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System;

namespace DataLoopDisplay.ViewModel
{


    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private SettingsReader settingsReader = new SettingsReader();
        private string excelFileName;
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int displayRowsPerLoop;
        private DataTable allrowsDataTable = null;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.excelFileName = this.settingsReader.GetExcelFileName();
            this.displayRowsPerLoop = this.settingsReader.GetDisplayRowsPerLoop();

            if (IsInDesignMode)
            {
                this.DataTableToDisplay = this.createFakeDatatable();
            }
            else
            {
                this.allrowsDataTable = this.ReadExcelToDataTable();

            }
            this.StartTimerShow();
            this.CreateFileWatcher(this.excelFileName);
            this.DisplayFontSize = this.settingsReader.GetDisplayFontSize();
        }

        public void CreateFileWatcher(string excelFileName)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Path.GetDirectoryName(excelFileName);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = Path.GetFileName(excelFileName);
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            this.dispatcherTimer.Tick -= new EventHandler(dispatcherTimer_Tick);
            this.allrowsDataTable = this.ReadExcelToDataTable();
            this.StartTimerShow();
        }

        private void StartTimerShow()
        {
            this.loopCount = 0;
            ShowLoop(this.allrowsDataTable, this.displayRowsPerLoop);
            this.dispatcherTimer.Interval = this.settingsReader.GetDisplaySecondsPerLoop();
            this.dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            this.dispatcherTimer.Start();
        }

        private int loopCount = 0;

        private void ShowLoop(DataTable all, int rowsper)
        {
            var rows = this.allrowsDataTable.Rows.OfType<DataRow>()
                .Skip(loopCount * displayRowsPerLoop).Take(displayRowsPerLoop);
            DataTable dt = this.allrowsDataTable.Clone();
            foreach (DataRow row in rows)
                dt.ImportRow(row);
            this.DataTableToDisplay = dt;
            this.loopCount++;
            if (this.loopCount >=
                this.allrowsDataTable.Rows.Count / this.displayRowsPerLoop)
                this.loopCount = 0;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ShowLoop(this.allrowsDataTable, this.displayRowsPerLoop);
        }

        private void FilterDataTableColumns(DataTable dt, IList<int> cols)
        {
            IList<int> allColumnsIndexes = Enumerable.Range(0, dt.Columns.Count).ToList();
            foreach (int i in (allColumnsIndexes.Except(cols)).OrderByDescending(x => x))
            {
                dt.Columns.RemoveAt(i);
            }
        }

        private void ReportDataTableRowColumnsCount(string excelFileName, DataTable dt)
        {
            int rowcnt = dt.Rows.Count;
            int colcnt = dt.Columns.Count;
            string msg = string.Format("{0},{1}行,{2}列",
                excelFileName, rowcnt, colcnt);
            this.Message = msg;
            Log.Instance.Logger.InfoFormat("msg");
        }

        private DataTable ReadExcelToDataTable()
        {
            if (!File.Exists(this.excelFileName))
            {
                string msg = $"找不到Excel文件{this.excelFileName}";
                MessageBox.Show(msg);
                Log.Instance.Logger.Error(msg);
                return new DataTable();
            }

            DataTable dt = ExcelDataReader.ReadToDataTable(this.excelFileName);
            this.ReportDataTableRowColumnsCount(this.excelFileName, dt);
            this.FilterDataTableColumns(dt, this.settingsReader.GetDisplayColumnIndexes());
            return dt;
        }

        private DataTable createFakeDatatable()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Name");
            dt.Columns.Add("Marks");
            DataRow ravi = dt.NewRow();
            ravi["Name"] = "ravi";
            ravi["Marks"] = "500";
            dt.Rows.Add(ravi);

            DataRow ly = dt.NewRow();
            ly["Name"] = "leiyang";
            ly["Marks"] = "1000";
            dt.Rows.Add(ly);

            return dt;
        }

        private DataTable dataTableToDisplay;
        public DataTable DataTableToDisplay
        {
            get
            {
                return this.dataTableToDisplay;
            }
            set
            {
                if (this.dataTableToDisplay != value)
                {
                    this.dataTableToDisplay = value;
                    this.RaisePropertyChanged(nameof(DataTableToDisplay));
                }
            }
        }

        private string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                if (this.message != value)
                {
                    this.message = value;
                    this.RaisePropertyChanged(nameof(Message));
                }
            }
        }

        private int displayFontSize;
        public int DisplayFontSize
        {
            get
            {
                return this.displayFontSize;
            }
            set
            {
                if (this.displayFontSize != value)
                {
                    this.displayFontSize = value;
                    this.RaisePropertyChanged(nameof(DisplayFontSize));
                }
            }
        }
    }
}