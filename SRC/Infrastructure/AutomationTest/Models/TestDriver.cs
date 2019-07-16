using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Common;
using Common.Helpers;
using Common.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTest.Models
{
    public class TestDriver<T>  where T : StartupClass
    {
        public void Run()
        {
            var startupInstance = Activator.CreateInstance(typeof(T));
            var startupMethod = typeof(T).GetMethod("Configures");
            startupMethod.Invoke(startupInstance, null);

            CommonHelper.WriteConsole("Reading Config...");

            //Read Config
            GlobalConfig config = null;
            try
            {
                config = GlobalConfig.Instant;
            }
            catch (Exception ex)
            {
                CommonHelper.WriteConsole($"Can't read Config. Exception: {ex.Message}", ConsoleType.Error);
                Console.ReadKey();
                return;
            }

            CommonHelper.WriteConsole("Setting up report test...");
            TestClass.htmlReporter = new ExtentHtmlReporter(config.Config.OutputTestFile == null ? Path.Combine(config.Config.ReportPath ?? Constant.ReportPath, Constant.TestReportFile) : Path.Combine(config.Config.ReportPath ?? Constant.ReportPath, config.Config.OutputTestFile));
            TestClass.extent = new AventStack.ExtentReports.ExtentReports();
            TestClass.extent.AttachReporter(TestClass.htmlReporter);

            foreach (var item in typeof(CommonConfig).GetProperties())
            {
                TestClass.extent.AddSystemInfo(item.Name, ((string)item.GetValue(config.Config))??"");
            }

            CommonHelper.WriteConsole("Test Running...");
            foreach (var testCase in config.TestCases)
            {
                ExtentTest testReport = null;
                if (!testCase.IsEnabled)
                {
                    testReport = TestClass.extent.CreateTest(testCase.Name, testCase.Description);
                    testReport.Skip($"'{testCase.Name}' had been skip by User");
                    continue;
                }

                var assembly = CommonHelper.GetAssemblyByName(testCase.AssemblyName);
                if(assembly != null)
                {
                    Type t = assembly.GetTypes().FirstOrDefault(tt => tt.Name.EndsWith(testCase.Class));
                    if (t != null)
                    {
                        var methodTest = t.GetMethod(testCase.Method);
                        if (methodTest != null)
                        {
                            var instance = Activator.CreateInstance(t);
                            var test = t.GetProperty("Test");
                            var driverPro = t.GetProperty("Driver");
                            var versionPro = t.GetProperty("DataVersion");
                            var testNamePro = t.GetProperty("TestName");
                            try
                            {
                                testNamePro.SetValue(instance, testCase.Name);
                                versionPro.SetValue(instance, testCase.DataVerion);
                                test.SetValue(instance, TestClass.extent.CreateTest(testCase.Name, testCase.Description));

                                methodTest.Invoke(instance, null);
                                var driver = driverPro.GetValue(instance);
                                if (driver != null)
                                {
                                    ((IWebDriver)driver).Close();
                                    ((IWebDriver)driver).Quit();
                                }
                                CommonHelper.WriteConsole($"Test Name {testCase.Name} has been successfull!");
                                continue;
                            }
                            catch (Exception Ex)
                            {
                                var failedTestMethod = t.BaseType.GetMethod("FailedTest");
                                failedTestMethod.Invoke(instance, new object[]{ Ex.InnerException == null ? Ex.Message : Ex.InnerException.Message  });
                                var driver = driverPro.GetValue(instance);
                                if (driver != null)
                                {
                                    ((IWebDriver)driver).Close();
                                    ((IWebDriver)driver).Quit();
                                }
                                CommonHelper.WriteConsole($"TestName {testCase.Name} failed with exception: { (Ex.InnerException == null ? Ex.Message : Ex.InnerException.Message) }", ConsoleType.Error);
                                continue;
                            }
                        }
                    }
                }

                testReport = TestClass.extent.CreateTest(testCase.Name, testCase.Description);
                testReport.Error($"TestName {testCase.Name} failed with exception: Can't load this testcase");
                CommonHelper.WriteConsole($"TestName {testCase.Name} failed with exception: Can't load this testcase", ConsoleType.Error);
            }

            CommonHelper.WriteConsole("Plush report test...");
            TestClass.extent.Flush();

            CommonHelper.WriteConsole("Test Completed");
            //Console.ReadLine();
        }
    }
}
