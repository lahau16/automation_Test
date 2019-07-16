using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Constant
    {
        public static string PATH_CONFIG = Path.Combine(Directory.GetCurrentDirectory(), "InputTest.xlsx");
        public const string SHEET_COMMONCONFIG = "Config";
        public const string SHEET_TESTCASE = "Testcase";
        public const string SHEET_KEYWORD = "Testkeyword";

        public const string TestReportFile = "Extentreporter.html";
        public const string ReportPath = "./ReportTest/";
    }
}
