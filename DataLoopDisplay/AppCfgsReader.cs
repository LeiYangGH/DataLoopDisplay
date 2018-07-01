using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLoopDisplay
{
    class AppCfgsReader
    {
        public static string GetExcelFileName()
        {
            return ConfigurationManager.AppSettings["excelfilename"];
        }
    }
}
