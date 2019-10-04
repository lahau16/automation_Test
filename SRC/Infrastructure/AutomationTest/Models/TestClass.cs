using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutomationTest.Models
{
    public abstract class TestClass
    {
        public static ExtentHtmlReporter htmlReporter = null;
        public static AventStack.ExtentReports.ExtentReports extent = null;

        public void Assert<T>(T input, T output)
        {
            if(input == null)
            {
                if(output != null)
                {
                    Test.Error($"Assert failed with value '{input}' and '{output}'");
                    throw new Exception($"Assert failed with value '{input}' # '{output}'");
                }
            }
            else 
            {
                if (!input.Equals(output))
                {
                    Test.Error($"Assert failed with value '{input}' and '{output}'");
                    AddScreenCaptureFromPath();

                    throw new Exception($"Assert failed with value '{input}' # '{output}'");
                }
                else
                {
                    PassTest($"Assert success with value '{input}' and '{output}'");
                }
                
            }
            

        }

        public void AssertNot<T>(T input, T output)
        {
            if (input.Equals(output))
            {
                Test.Error($"AssertNot failed with value '{input}' and '{output}'");
                AddScreenCaptureFromPath();
                throw new Exception($"Assert failed with value '{input}' # '{output}'");

            }
            else
            {
                PassTest($"AssertNot success with value '{input}' and '{output}'");
                AddScreenCaptureFromPath();
            }
        }

        public IWebDriver Driver { set; get; } = null;
        public ExtentTest Test { set; get; } = null;
        public string DataVersion { set; get; }
        public string TestName { set; get; }
        public Dictionary<string, string> TestKeyWords => GlobalConfig.Instant.TestKeywords[DataVersion][TestName];
        public Dictionary<string, string> TestCommonKeyWords => GlobalConfig.Instant.TestKeywords[DataVersion]["Common"];

        public void AddSystemInfo(string key, string value)
        {
            extent.AddSystemInfo(key, value);
        }

        public void AddScreenCaptureFromPath()
        {
            if (Driver != null)
            {
                ITakesScreenshot ts = (ITakesScreenshot)Driver;
                Screenshot screenshoot = ts.GetScreenshot();
                string path = Path.Combine(Constant.ReportPath, GlobalConfig.Instant.Config.ReportPath ?? string.Empty, "ScreenShots/");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string imageName = Guid.NewGuid().ToString() + ".png";
                string localpath = path + imageName;
                screenshoot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
                Test.AddScreenCaptureFromPath(Path.Combine(GlobalConfig.Instant.Config.ReportPath ?? string.Empty, "ScreenShots/") + imageName);
            }
        }

        public void Infor(string message)
        {
            Test.Info(message);
        }

        public void PassTest(string message)
        {
            Test.Pass(message);
        }

        public void SkipTest(string message)
        {
            Test.Skip(message);
        }

        public void FailedTest(string message)
        {
            Test.Fail(message);
        }

        public void WarningTest(string message)
        {
            Test.Warning(message);
        }

        #region Selenium Support
        public void GotoElement(By by)
        {
            var element = Driver.FindElement(by);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
        public bool IsExist(By by)
        {
            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void TryAssertWithCatch(Action callback)
        {
            try
            {
                callback();
            }
            catch { }
        }
        #endregion
    }
}
