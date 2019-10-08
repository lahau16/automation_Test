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
using AutomationTest.Extensions;
using System.Collections.Generic;
//using AutoItX3Lib;

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
            Thread.Sleep(10000);
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
        public void Add_item()
        {
            Login();

            Infor("Select item ");
            Driver.FindElement(By.XPath(TestCommonKeyWords["MasterDataSetting"])).Click();
            Driver.GotoElement(By.XPath(TestCommonKeyWords["Items"]));
            Driver.FindElement(By.XPath(TestCommonKeyWords["Items"])).Click();

            if (Driver.IsExist(By.CssSelector(TestCommonKeyWords["Theme"])))
            {
                Driver.FindElement(By.CssSelector(TestCommonKeyWords["Theme"])).Click();
            }


            Driver.FindElement(By.XPath(TestKeyWords["Button.Add"])).Click();
            Driver.Wait(5000);

            Infor("1. Tab general information ");
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Switchbutton.Active"])).Click();

            Infor("1.1. Update avartar");
            Driver.OpenFile(By.XPath(TestKeyWords["Xpath.Avartar"]), TestKeyWords["Xpath.Avartar.File"]);
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
            Driver.GotoElement(By.XPath(TestKeyWords["Xpath.Button.Save"]));
            Driver.FindElement(By.XPath(TestKeyWords["Xpath.Button.Save"])).Click();
            Infor("Result");
            Thread.Sleep(1000);
            CommonHelper.WriteConsole(Driver.FindElement(By.CssSelector(TestCommonKeyWords["Class.Toast"])).Text);
            Assert((Driver.FindElement(By.CssSelector(TestCommonKeyWords["Class.Toast"])).Text), "Common.AddSuccess");
            Assert(Driver.Url.Trim(), TestKeyWords["Assert.Link"].Trim());
            Thread.Sleep(2000);
            var searchbox = Driver.FindElement(By.XPath(TestKeyWords["Xpath.List.Search"]));
            searchbox.SendKeys(itemName);
            searchbox.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            AddScreenCaptureFromPath();

            // Đếm số lượng dòng trong trang hiện tại
            //*[@id="app"]/div[1]/main/div/div/div[2]/div[2]/div[3]/table/tbody/tr
            //Infor("Count record: " + Driver.ElementCount(By.XPath("*[@id=\"app\"]/div[1]/main/div/div/div[2]/div[2]/div[3]/table/tbody/tr")));
    }
        public void List_item()
        {
            Login();

            Infor("Select item ");
            Driver.FindElement(By.XPath(TestCommonKeyWords["MasterDataSetting"])).Click();
            Driver.GotoElement(By.XPath(TestCommonKeyWords["Items"]));
            Driver.FindElement(By.XPath(TestCommonKeyWords["Items"])).Click();
            if (Driver.IsExist(By.CssSelector(TestCommonKeyWords["Theme"])))
            {
                Driver.FindElement(By.CssSelector(TestCommonKeyWords["Theme"])).Click();
            }

            // check paging
            var result = Driver.GetTableInformation(10);
            Infor("TotalPages: " + result.TotalPages);
            Infor("TotalRecords: " + result.TotalRecords);
            //Infor("Check function search");
            //var searchbox = Driver.FindElement(By.XPath(TestKeyWords["Xpath.List.Search"]));
            //searchbox.SendKeys(TestKeyWords["Data.List.Search_1"]);
            //Infor($"Search chính xác dữ liệu" + searchbox.Text);
            //Driver.Wait(3000);
            //AddScreenCaptureFromPath();
            //searchbox.Clear();

            //searchbox.SendKeys(TestKeyWords["Data.List.Search_2"]);
            //Infor($"Search gần đúng" + searchbox.Text);
            //Driver.Wait(3000);
            //AddScreenCaptureFromPath();
            //searchbox.Clear();

            //searchbox.SendKeys(TestKeyWords["Data.List.Search_3"]);
            //Infor($"Search không ra kết quả" + searchbox.Text);
            //Driver.Wait(3000);
            //AddScreenCaptureFromPath();
            //searchbox.Clear();

            //Infor("Paging");
            //if (Driver.IsExist(By.CssSelector(TestKeyWords["CssSelector.Paging"])))
            //{
            //    // Kiểm tra số dòng dữ liệu trên 1 trang và tính ra sẽ có bao nhiêu trang. 
            //    int countPaging = Driver.ElementCount(By.XPath(TestKeyWords["Xpath.Paging"]));
            //    Infor($"So page:" + countPaging);

            //}



        }
    }
}
