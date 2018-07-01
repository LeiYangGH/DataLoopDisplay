using GalaSoft.MvvmLight;
using System.Data;
using ExcelReader;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                this.DataTableToDisplay = this.createFakeDatatable();
            }
            else
            {
                this.DataTableToDisplay = this.ReadExcelToDataTable();

            }
        }

        private void FilterDataTableColumns(DataTable dt, IList<int> cols)
        {
            IList<int> allColumnsIndexes = Enumerable.Range(0, dt.Columns.Count).ToList();
            foreach (int i in (allColumnsIndexes.Except(cols)).OrderByDescending(x => x))
            {
                dt.Columns.RemoveAt(i);
            }
        }

        private DataTable ReadExcelToDataTable()
        {
            string excelFileName = AppCfgsReader.GetExcelFileName();
            if (!File.Exists(excelFileName))
            {
                MessageBox.Show($"找不到文件{excelFileName}");
                return new DataTable();
            }

            DataTable dt = ExcelDataReader.ReadToDataTable(excelFileName);
            this.FilterDataTableColumns(dt, AppCfgsReader.GetDisplayColumnIndexes());
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
    }
}