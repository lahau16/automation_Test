using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CommonConfig
    {
        public string TestName { set; get; }
        public string Version { set; get; }
        public string OutputTestFile { set; get; }
        public string ReportPath { set; get; }
        public string Type { set; get; }

        public DriverType DriverTypeEnum => (DriverType)Enum.Parse(typeof(DriverType), Type);
    }
}
