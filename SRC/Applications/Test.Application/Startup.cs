using AutomationTest.Models;
using Common.Extensions;
using Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Application
{
    public class Startup : StartupClass
    {
        public override void Configures()
        {
            TestExtensions.LoadInstalledModules(Directory.GetCurrentDirectory());
        }
    }
}
