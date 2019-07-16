using AutomationTest.Helpers;
using AutomationTest.Models;
using System.Dynamic;
using Test.UI.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;

namespace Test.TestCases
{
    public class HCM_LoginSuscees : TestClass

    {
        public void LoginSuscees()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestKeyWords["Login.URL"]
            };
            Thread.Sleep(3000);

            //Login suscees
            Driver.FindElement(By.XPath(TestKeyWords["Login.Username.ID"])).SendKeys(TestKeyWords["Login.Username.Data"]);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath(TestKeyWords["Login.Pass.ID"])).SendKeys(TestKeyWords["Login.Pass.Data"]);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath(TestKeyWords["Login.Submit"])).Click();
            Thread.Sleep(2000);
        }
    }
}
