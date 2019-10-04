using AutomationTest.Models;
using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Admin.TestCases
{
    public class DMS_AI : TestClass
    {
        public void Login ()
        {

            Driver.Navigate().GoToUrl(TestKeyWords["Url"]);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Infor("Input username and password");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.User"])).SendKeys(TestKeyWords["Xpath.UserData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Pass"])).SendKeys(TestKeyWords["Xpath.PassData"]);
            Infor("Click button login");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Button"])).Click();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert(Driver.Url.Trim(), TestKeyWords["Assert.Link"].Trim());
            Thread.Sleep(10000);

        }

        public void Logout()
        {
            Infor("Log out");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Driver.FindElement(By.XPath(TestKeyWords["Logout.Action1"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Logout.Action2"])).Click();
            Assert(Driver.Url.Trim(), TestKeyWords["URL"].Trim());
        }
        public void Add_Item()
        {
            Login();
            Infor("Select item ");
            Driver.FindElement(By.XPath(TestCommonKeyWords["MasterDataSetting"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Items"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Theme"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Button.Add"])).Click();
        }
    }
}
