using System;
using System.Collections.Generic;
using System.Text;
using AutomationTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace Test.TestCases
{
    public class Test_Guru99 : TestClass
    {
        public void Testcase01()
        {
            Infor("Mở trình duyệt");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Infor("Maximize trình duyệt");
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            Infor("Láy text");
            String demosite = Driver.FindElement(By.CssSelector(TestKeyWords["Guru.Text"])).Text;
            Console.WriteLine(demosite);
            Assert(demosite.Trim(), TestKeyWords["Text.Test"].Trim());
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Mobile"])).Click();
            new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Sortby.Mobile"]))).SelectByText(TestKeyWords["Guru.Sortby.Name"]);

            AddScreenCaptureFromPath();
            PassTest("Hải hay quá");
        }

        public void Testcase02()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Mobile"])).Click();
            String PriceSony = Driver.FindElement(By.XPath(TestKeyWords["Guru.Price.Sony"])).Text;
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Detail.Sony"])).Click();
            String DetaiPriceSony = Driver.FindElement(By.XPath(TestKeyWords["Guru.Detail.Price.Sony"])).Text;
            Assert(PriceSony, DetaiPriceSony);
        }
    }
}

