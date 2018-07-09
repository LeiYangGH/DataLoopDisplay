using GalaSoft.MvvmLight;
using System.Data;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System;
using ExcelPdfJpg;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DataLoopDisplay.Views;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;

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

            if (IsInDesignMode)
            {
                
            }
            else
            {
               
                this.LoopViewExcel(this.excelFileName);
            }
            //this.StartTimerShow();
            this.CreateFileWatcher(this.excelFileName);
            //this.DisplayFontSize = this.settingsReader.GetDisplayFontSize();
        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        public async Task LoopViewExcel(string excelFileName)
        {
            await Task.Run(() =>
            {
                this.Message = $"正在转换{excelFileName}...";
                string tempFolder = this.GetTemporaryDirectory();
                string tempExcelFile = Path.Combine(tempFolder, Path.GetFileName(excelFileName));
                File.Copy(excelFileName, tempExcelFile);
                string xps = ExcelToXpsConverter.Convert(tempExcelFile);

                this.Message = $"正在显示{xps}...";
                //this.ImageFileName = jpg;
                if (App.Current != null)//walkaround
                    App.Current.Dispatcher.BeginInvoke(new Action(
                        () =>
                        {
                            XpsDocument xpsDocument = new XpsDocument(xps, FileAccess.Read);
                            FixedDocumentSequence fds = xpsDocument.GetFixedDocumentSequence();
                            MainWindow.mainWindow.docViewer.Document = fds;
                            this.Message = $"当前显示{excelFileName}！";
                        }));
                this.Message = $"正在加载{xps}！";
            });
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

            this.LoopViewExcel(this.excelFileName);
        }

        void SetMovieSource(string path)
        {

            var myImage = new BitmapImage();
            myImage.BeginInit();
            myImage.UriSource = new Uri(path);
            myImage.EndInit();
            this.MovieImageSource = myImage;
        }

        private ImageSource movieImageSource;

        public ImageSource MovieImageSource
        {
            get
            {
                return this.movieImageSource;
            }
            set
            {
                if (this.movieImageSource != value)
                {
                    this.movieImageSource = value;
                    this.RaisePropertyChanged(nameof(MovieImageSource));
                }
            }
        }

        private string imageFileName;
        public string ImageFileName
        {
            get
            {
                return this.imageFileName;
            }
            set
            {
                if (this.imageFileName != value)
                {
                    this.imageFileName = value;
                    this.RaisePropertyChanged(nameof(ImageFileName));
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

    }
}