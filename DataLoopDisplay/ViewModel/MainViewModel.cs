using GalaSoft.MvvmLight;
using System.Data;
using ExcelReader;
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


        private DataTable ReadExcelToDataTable()
        {
            string excelFileName = AppCfgsReader.GetExcelFileName();
            return ExcelDataReader.ReadToDataTable(excelFileName);
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