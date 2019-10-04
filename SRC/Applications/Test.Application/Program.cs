using Admin.Application;
using AutomationTest.Models;
using System;

namespace Test.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFileName = args.Length > 0 ? args[0]: "";
            (new TestDriver<Startup>()).Run(inputFileName);
        }
    }
}
