using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DataLoopDisplay
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
          string appdataDir = Path.Combine(Environment.GetFolderPath(
              Environment.SpecialFolder.ApplicationData), Constants.Application_Name);
            if (!Directory.Exists(appdataDir))
                Directory.CreateDirectory(appdataDir);
            base.OnStartup(e);
        }
    }
}
