using Common.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GlobalConfig
    {
        private static GlobalConfig _Instant = null;
        public static GlobalConfig Instant
        {
            get
            {
                if (_Instant == null)
                {
                    _Instant = new GlobalConfig();
                    _Instant.Read();
                }
                return _Instant;
            }
        }

        public static IList<ModuleInfo> Modules { get; set; }

        public CommonConfig Config { private set; get; } = new CommonConfig();
        public List<TestCase> TestCases { private set; get; } = new List<TestCase>();
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> TestKeywords { private set; get; } = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        public void Read()
        {

            //Read config file
            //HSSFWorkbook hssfwb;
            IWorkbook workbook = new XSSFWorkbook();
            using (FileStream file = new FileStream(Constant.PATH_CONFIG, FileMode.Open, FileAccess.Read))
            {
                //hssfwb = new HSSFWorkbook(file);
                workbook = new XSSFWorkbook(file);
            }

            // Get Sheet Common Config
            ISheet sheet = workbook.GetSheet(Constant.SHEET_COMMONCONFIG);
            if(sheet == null)
            {
                throw new Exception($"Sheet {Constant.SHEET_COMMONCONFIG} isn't exsited.");
            }

            //Read Common Config
            var cPros = typeof(CommonConfig).GetProperties();
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                var key = sheet.GetRow(row).GetCell(0).StringCellValue;
                var pro = cPros.FirstOrDefault(p => p.Name == key);
                if(pro != null)
                {
                    pro.SetValue(Config, sheet.GetRow(row).GetCell(1).StringCellValue);
                }
            }

            // Get Sheet TestCase
            sheet = workbook.GetSheet(Constant.SHEET_TESTCASE);
            if (sheet != null)
            {
                //Read TestCase
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    if(sheet.GetRow(row).Cells.Count() != 7)
                    {
                        continue;
                    }
                    var testCase = new TestCase()
                    {
                        Name = sheet.GetRow(row).GetCell(0).StringCellValue,
                        DataVerion = sheet.GetRow(row).GetCell(1).StringCellValue,
                        AssemblyName = sheet.GetRow(row).GetCell(2).StringCellValue,
                        Class = sheet.GetRow(row).GetCell(3).StringCellValue,
                        Method = sheet.GetRow(row).GetCell(4).StringCellValue,
                        Description = sheet.GetRow(row).GetCell(5).StringCellValue,
                        IsEnabled = sheet.GetRow(row).GetCell(6).BooleanCellValue
                    };
                    TestCases.Add(testCase);
                }
            }

            // Get Sheet Keyword
            sheet = workbook.GetSheet(Constant.SHEET_KEYWORD);
            if (sheet != null)
            {
                //Read Keyword
                for (int row = 1; row <= sheet.LastRowNum; row++)
                {
                    if (sheet.GetRow(row).Cells.Count() != 4)
                    {
                        continue;
                    }
                    var testName = sheet.GetRow(row).GetCell(0).StringCellValue;
                    var dataVersion = sheet.GetRow(row).GetCell(1).StringCellValue;
                    if (TestKeywords.Keys.FirstOrDefault(k => k == dataVersion) == null)
                    {
                        TestKeywords[dataVersion] = new Dictionary<string, Dictionary<string, string>>();
                    }
                    if(TestKeywords[dataVersion].Keys.FirstOrDefault(k => k == testName) == null)
                    {
                        TestKeywords[dataVersion][testName] = new Dictionary<string, string>();
                    }

                    var key = sheet.GetRow(row).GetCell(2).StringCellValue;
                    var data = sheet.GetRow(row).GetCell(3).StringCellValue;
                    TestKeywords[dataVersion][testName][key] = data;
                }
            }
        }
    }
}
