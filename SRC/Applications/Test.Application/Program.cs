using Admin.Application;
using AutomationTest.Models;
using System;

namespace Test.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            (new TestDriver<Startup>()).Run();
            //Console.WriteLine("1111");
            //Console.ReadKey();
        }
    }
}
