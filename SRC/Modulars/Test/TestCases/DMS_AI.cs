using AutomationTest.Models;
using Common;
using Common.Helpers;
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

            Driver.Navigate().GoToUrl(TestCommonKeyWords["Url"]);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Infor("Input username and password");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Xpath.User"])).SendKeys(TestCommonKeyWords["Xpath.UserData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Xpath.Pass"])).SendKeys(TestCommonKeyWords["Xpath.PassData"]);
            Infor("Click button login");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Xpath.Button"])).Click();
            //Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(5000);
            Assert(Driver.Url.Trim(), TestCommonKeyWords["Assert.Link"].Trim());
            Thread.Sleep(1000);

        }

        public void Logout()
        {
            Infor("Log out");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Logout.Action1"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Logout.Action2"])).Click();
            Assert(Driver.Url.Trim(), TestCommonKeyWords["URL"].Trim());
        }
        public void Items()
        {
            Login();
            Infor("Select item ");
            Driver.FindElement(By.XPath(TestKeyWords["MasterDataSetting"])).Click();
            GotoElement(By.XPath(TestKeyWords["Items"]));
            Driver.FindElement(By.XPath(TestKeyWords["Items"])).Click();

            if (IsExist(By.CssSelector(TestKeyWords["Theme"])))
            {
                Driver.FindElement(By.CssSelector(TestKeyWords["Theme"])).Click();
            }
            Driver.FindElement(By.XPath(TestKeyWords["Button.Add"])).Click();
            Thread.Sleep(5000);
            var itemCode = Guid.NewGuid().ToString().Substring(0, 5);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.Code"])).SendKeys(itemCode);
            var itemName = Guid.NewGuid().ToString();
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.Name"])).SendKeys(itemName);
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/main/div/div/div/div/div/div[2]/form/div[2]/div[1]/div[3]/div[1]/div/div/div[1]/input")).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/main/div/div/div/div/div/div[2]/form/div[2]/div[1]/div[3]/div[1]/div/div/div[2]/div[1]/div[1]/ul/li[1]")).Click();
            Thread.Sleep(1000);

            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/main/div/div/div/div/div/div[2]/form/div[2]/div[1]/div[3]/div[3]/div/div/div[1]/input")).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//*[@id='app']/div[1]/main/div/div/div/div/div/div[2]/form/div[2]/div[1]/div[3]/div[3]/div/div/div[2]/div[1]/div[1]/ul/li[1]")).Click();
            Thread.Sleep(1000);



            GotoElement(By.XPath(TestKeyWords["Xpath.Button.Save"]));
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Button.Save"])).Click();
            Thread.Sleep(1000);
            CommonHelper.WriteConsole(Driver.FindElement(By.CssSelector(TestCommonKeyWords["Class.Toast"])).Text);
            Assert(Driver.Url.Trim(), TestKeyWords["Assert.Link"].Trim());

            // Verify


        }
    }
}
