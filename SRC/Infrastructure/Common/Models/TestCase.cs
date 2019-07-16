using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TestCase
    {
        public string Name { set; get; }
        public string DataVerion { set; get; }
        public string AssemblyName { set; get; }
        public string Class { set; get; }
        public string Method { set; get; }
        public string Description { set; get; }
        public bool IsEnabled { set; get; }
    }
}
