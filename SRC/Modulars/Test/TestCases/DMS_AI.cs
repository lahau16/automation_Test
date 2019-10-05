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
using AutoItX3Lib;

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

            Infor("1. Tab general information ");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Switchbutton.Active"])).Click();

            Infor("1.1. Update avartar");
            var avatar = Driver.FindElement(By.XPath(TestKeyWords["Xpath.Avartar"]));
            avatar.Click();
            Thread.Sleep(5000);
            AutoItX3 autoIT = new AutoItX3();

            // đưa title của cửa sổ File upload vào chuỗi. 
            // Cửa sổ hiện ra có thể có tiêu đề là File Upload hoặc Tải lên một tập tin
            // lấy ra cửa sổ active có tiêu đề như dưới
            autoIT.WinActivate("Open");

            // file data nằm trong thư mục debug
            // gửi link vào ô đường dẫn
            autoIT.Send(TestKeyWords["Xpath.Avartar.File"]);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            // gửi phím enter sau khi truyền link vào
            autoIT.Send("{ENTER}");
            Thread.Sleep(5000);
            AddScreenCaptureFromPath();


            Infor("1.2. Insert Code, Name auto generate data");
            Thread.Sleep(1000);
            var itemCode = Guid.NewGuid().ToString().Substring(0, 5);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.Code"])).SendKeys(itemCode);
            var itemName = Guid.NewGuid().ToString();
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.Name"])).SendKeys(itemName);

            Infor("1.3. Insert Foreign, Description, Barcode");
            var ForeignName = Guid.NewGuid().ToString();
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.ForeignName"])).SendKeys(ForeignName);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.Description"])).SendKeys(TestKeyWords["Data.Description"]);
            var barcode = Guid.NewGuid().ToString().Substring(2,13);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.TextBox.Barcode"])).SendKeys(barcode);

            Infor("1.4. Insert item type, UoM group, UoM inventory");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.ItemType"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.ItemType"])).Click();
            Thread.Sleep(1000);

            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.UoM"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.UoM"])).Click();
            Thread.Sleep(1000);

            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.UoMInventory"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.UoMInventory"])).Click();
            Thread.Sleep(1000);

            Infor("1.5. Insert price list, pricing unit, unit price");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.Pricelist"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.Pricelist"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.PricingUnit"])).SendKeys(TestKeyWords["Data.PricingUnit"]);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.UnitPrice"])).SendKeys(TestKeyWords["Data.UnitPrice"]);

            Infor("1.6. Sale information");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.SaleInfor.UOMCode"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.SaleInfor.UOMCode"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.SaleInfor.TaxGroup"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.SaleInfor.TaxGroup"])).Click();
            Thread.Sleep(1000);
            Infor("1.7. Purchasing information");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.Purchasinginfor.UOMCode"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.Purchasinginfor.UOMCode"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.Purchasinginfor.TaxGroup"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["DataPurchasinginfor.TaxGroup"])).Click();
            Thread.Sleep(1000);

            Infor("2. Tab Item group ");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Tab.Itemgroup"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.Itemgroup"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.Itemgroup"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Dropdown.Attribute1"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Data.Attribute1"])).Click();
            Thread.Sleep(1000);

            Infor("Click submit");
            GotoElement(By.XPath(TestKeyWords["Xpath.Button.Save"]));
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Button.Save"])).Click();
            Infor("Result");
            Thread.Sleep(1000);
            CommonHelper.WriteConsole(Driver.FindElement(By.CssSelector(TestCommonKeyWords["Class.Toast"])).Text);
            Assert((Driver.FindElement(By.CssSelector(TestCommonKeyWords["Class.Toast"])).Text), "Add thành công");
            Assert(Driver.Url.Trim(), TestKeyWords["Assert.Link"].Trim());
            Thread.Sleep(2000);
            var searchbox = Driver.FindElement(By.XPath(TestKeyWords["Xpath.List.Search"]));
            searchbox.SendKeys(itemName);
            searchbox.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            AddScreenCaptureFromPath();




            // Verify


        }
    }
}
