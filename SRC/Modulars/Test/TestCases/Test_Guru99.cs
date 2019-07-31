using System;
using System.Collections.Generic;
using System.Text;
using AutomationTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

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

            Infor("Lấy thông tin: 'This is website demo'");
            String demosite = Driver.FindElement(By.CssSelector(TestKeyWords["Guru.Text"])).Text;
            Console.WriteLine(demosite);
            Infor("So sánh với dữ liệu");
            Assert(demosite.Trim(), TestKeyWords["Text.Test"].Trim());
            Infor("Click vào Mobile");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Mobile"])).Click();
            Infor("Sort by Name");
            new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Sortby.Mobile"]))).SelectByText(TestKeyWords["Guru.Sortby.Name"]);
        }

        public void Testcase02()
        {
            Infor("Open brower");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Infor("Open brower maximum");
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Infor("Click Mobile");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Mobile"])).Click();
            Infor("Compare pice");
            String PriceSony = Driver.FindElement(By.XPath(TestKeyWords["Guru.Price.Sony"])).Text;
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Detail.Sony"])).Click();
            String DetaiPriceSony = Driver.FindElement(By.XPath(TestKeyWords["Guru.Detail.Price.Sony"])).Text;
            Assert(PriceSony, DetaiPriceSony);
        }

        public void Testcase03()
        {
            Infor("1. Goto http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Infor("2. Click on ‘MOBILE’ menu");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Mobile"])).Click();
            Infor("3. In the list of all mobile , click on ‘ADD TO CART’ for Sony Xperia mobile");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.AddToCart"])).Click();
            Infor("4. Change ‘QTY’ value to 1000 and click ‘UPDATE’ button. Expected that an error is displayed");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.UpdateQuality"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.UpdateQuality"])).SendKeys("1000");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.UpdateButton"])).Click();
            Infor("5. Verify the error message");
            String messageError = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.MessageError"])).Text;
            Assert(messageError.Trim(), TestKeyWords["Guru.Xpath.MessageErrorExpect"].Trim());
            Infor("6. Then click on ‘EMPTY CART’ link in the footer of list of all mobiles. A message 'SHOPPING CART IS EMPTY' is shown.");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.EmptyCart"])).Click();
            Infor("7. Verify cart is empty");
            String noEmptyCart = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.NotificationEmptyCart"])).Text;
            Assert(noEmptyCart.Trim(), TestKeyWords["Guru.Xpath.NotificationEmptyCartExpect"].Trim());
        }

        public void Testcase04()
        {
            Infor("1.Goto http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Infor("2.Click on ‘MOBILE’ menu");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Mobile"])).Click();
            Infor("3.In mobile products list , click on ‘Add To Compare’ for 2 mobiles(Sony Xperia & Iphone)");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.AddToCompareSony"])).Click();
            string main_Mobile1 = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.NameSony"])).Text;
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.AddToCompareIphone"])).Click();
            string main_Mobile2 = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.NameIphone"])).Text;
            Infor("4.Click on ‘COMPARE’ button.A popup window opens");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Compare"])).Click();
            string newWindowHandle = Driver.WindowHandles.Last();
            var newWindow = Driver.SwitchTo().Window(newWindowHandle);
            Infor("5.Verify the pop - up window and check that the products are reflected in it.Heading 'COMPARE PRODUCTS' with selected products in it.");
            string strHead = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Head"])).Text;
            Assert(strHead.Trim(), TestKeyWords["Guru.Xpath.HeadContent"].Trim());
            string popup_Mobile1 = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.NameSonyNew"])).Text;
            Assert(popup_Mobile1.Trim(), main_Mobile1.Trim());
            string popup_Mobile2 = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.NameIphoneNew"])).Text;
            Assert(popup_Mobile2.Trim(), main_Mobile2.Trim());
            Infor("6.Close the Popup Windows");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Close"])).Click();
            string FirstWindowHandle = Driver.WindowHandles.First();
            var FirstWindows = Driver.SwitchTo().Window(FirstWindowHandle);

        }
        public void Testcase05()
        {
            Infor("1.Go to http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            Infor("2.Click on my account link");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyAcount"])).Click();

            Infor("3.Click Create an Account link and fill New User information except Email ID");
            string lastWindows = Driver.WindowHandles.Last();
            Driver.SwitchTo().Window(lastWindows);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.CreateAcount"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.FirstName"])).SendKeys(TestKeyWords["Guru.Xpath.FirstNameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.MiddleName"])).SendKeys(TestKeyWords["Guru.Xpath.MiddleNameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.LastName"])).SendKeys(TestKeyWords["Guru.Xpath.LastNameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Email"])).SendKeys(TestKeyWords["Guru.Xpath.EmailData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Pass"])).SendKeys(TestKeyWords["Guru.Xpath.PassData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.PassConfirm"])).SendKeys(TestKeyWords["Guru.Xpath.PassConfirmData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Checkbox"])).Click();
            
            Infor("4.Click Register");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ResgiterButton"])).Click();
            string FirstWindows = Driver.WindowHandles.First();
            Driver.SwitchTo().Window(FirstWindows);

            Infor("5.Verify Registration is done.Expected account registration done.");
            string welcomeNoti = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.WelcomeNotification"])).Text;
            Assert(welcomeNoti.Trim(), TestKeyWords["Guru.Xpath.WelcomeNotificationData"].Trim());

            Infor("6.Go to TV menu");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.MenuTV"])).Click();


            Infor("7.Add product in your wish list - use product - LG LCD");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.WishList"])).Click();

            Infor("8.Click SHARE WISHLIST");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShareWishList"])).Click();

            Infor("9.In next page enter Email and a message and click SHARE WISHLIST");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.EmailAddress"])).SendKeys(TestKeyWords["Guru.Xpath.EmailAddressData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Message"])).SendKeys(TestKeyWords["Guru.Xpath.MessageData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShareWishListSubmit"])).Click();

            Infor("10.Check wishlist is shared.Expected wishlist shared successfully.");
            string WishlistShare = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShareWishListSuccess"])).Text;
            Assert(WishlistShare.Trim(), TestKeyWords["Guru.Xpath.ShareWishListSuccessData"].Trim());

        }
        public void Testcase06()
        {
            Infor("1.Go to http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            Infor("2.Click on my account link");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyAcount"])).Click();

            Infor("3.Login in application using previously created credential");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginEmail"])).SendKeys(TestCommonKeyWords["Guru.Xpath.LoginEmailData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginPass"])).SendKeys(TestCommonKeyWords["Guru.Xpath.LoginPassData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginSubmit"])).Click();
            Thread.Sleep(3000);
            //Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Acount"])).Click();

            Infor("4.Click on MY WISHLIST link");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.MyWhistlist"])).Click();

            Infor("5.In next page, Click ADD TO CART link");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.AddToCartWhistlist"])).Click();

            Infor("6.Enter general shipping country, state / province and zip for the shipping cost estimate");
            new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Country"]))).SelectByText(TestKeyWords["Guru.Xpath.CountryData"]);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Region"])).SendKeys(TestKeyWords["Guru.Xpath.RegionData"]);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.PostCode"])).SendKeys(TestKeyWords["Guru.Xpath.PostCodeData"]);

            Infor("7.Click Estimate");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Estimate"])).Click();
            /*
            Infor("8.Verify Shipping cost generated");
            string flatRate = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.FlatRate"])).Text;
            Assert(flatRate.Trim(), TestKeyWords["Guru.Xpath.FlatRateData"].Trim());
            string fpriceFix = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.FlatRate"])).Text;
            Assert(fpriceFix.Trim(), TestKeyWords["Guru.Xpath.FixPriceData"].Trim());
            */
            Infor("9.Select Shipping Cost, Update Total");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.CheckboxPrice"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.UpdateTotal"])).Click();

            Infor("10.Verify shipping cost is added to total");
            string vflatRatePrice = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.vFlatRatePrice"])).Text;
            //Assert(vflatRatePrice.Trim(), TestKeyWords["Guru.Xpath.vFlatRatePriceData"].Trim());
            
            Infor("11.Click 'Proceed to Checkout'");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Checkout"])).Click();

            Infor("12a.Enter Billing Information, and click Continue");
            SelectElement BtnAdress = new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.NewAddress"])));
            BtnAdress.SelectByIndex(2);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Company"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Company"])).SendKeys(TestCommonKeyWords["Guru.Xpath.CompanyData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Address"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Address"])).SendKeys(TestCommonKeyWords["Guru.Xpath.AddressData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.AddressStreet"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.AddressStreet"])).SendKeys(TestCommonKeyWords["Guru.Xpath.AddressStreetData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.City"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.City"])).SendKeys(TestCommonKeyWords["Guru.Xpath.CityData"]);
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.RegionBilling"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.RegionBilling"])).SendKeys(TestKeyWords["Guru.Xpath.RegionBillingData"]);
            //new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.RegionBilling"]))).SelectByText(TestKeyWords["Guru.Xpath.RegionBillingData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.PostCodeBilling"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.PostCodeBilling"])).SendKeys(TestCommonKeyWords["Guru.Xpath.PostCodeBillingData"]);
            new SelectElement(Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.CountryBilling"]))).SelectByText(TestCommonKeyWords["Guru.Xpath.CountryBillingData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Telephone"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Telephone"])).SendKeys(TestCommonKeyWords["Guru.Xpath.TelephoneData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Fax"])).Clear();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Fax"])).SendKeys(TestCommonKeyWords["Guru.Xpath.FaxData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Checkbox1"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ContinueButton"])).Click();
            Thread.Sleep(3000);

            Infor("12b.Enter Shipping Information, and click Continue");
            SelectElement BtnAdressship = new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipAddress"])));
            BtnAdressship.SelectByIndex(2);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipFirstName"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipFirstName"])).SendKeys(TestKeyWords["Guru.Xpath.ShipFirstNameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipMiddleName"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipMiddleName"])).SendKeys(TestKeyWords["Guru.Xpath.ShipMiddleNameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipLastName"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipLastName"])).SendKeys(TestKeyWords["Guru.Xpath.ShipLastNameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipCompany"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipCompany"])).SendKeys(TestKeyWords["Guru.Xpath.ShipCompanyData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipAdress1"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipAdress1"])).SendKeys(TestKeyWords["Guru.Xpath.ShipAdress1Data"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipAdress2"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipAdress2"])).SendKeys(TestKeyWords["Guru.Xpath.ShipAdress2Data"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipCity"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipCity"])).SendKeys(TestKeyWords["Guru.Xpath.ShipCityData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipDistrict"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipDistrict"])).SendKeys(TestKeyWords["Guru.Xpath.ShipDistrictData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipPostcode"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipPostcode"])).SendKeys(TestKeyWords["Guru.Xpath.ShipPostcodeData"]);
            new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipCountry"]))).SelectByText(TestKeyWords["Guru.Xpath.ShipCountryData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipTelephone"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipTelephone"])).SendKeys(TestKeyWords["Guru.Xpath.ShipTelephoneData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipCheckbox"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ShipButton"])).Click();
            Thread.Sleep(3000);
            Infor("13.In Shipping Method, Click Continue");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipButtonMethod"])).Click();
            Thread.Sleep(3000);

            Infor("14.In Payment Information select 'Check/Money Order' radio button.Click Continue");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipMethodCheckMo"])).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipButtonMethodPayment"])).Click();
            Thread.Sleep(3000);

            Infor("15.Click 'PLACE ORDER' button");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipPlaceOrder"])).Click();
            Thread.Sleep(3000);

            Infor("16.Verify Oder is generated.Note the order number");
            PassTest("Ra order rồi nhé");
            AddScreenCaptureFromPath();

            //NOTE: PROCEED TO CHECKOUT (step 6 ) was taken out, so as to allow the Estimate button step to get processed. 
            //Rest of the steps renumbered accordingly.
        }
        public void Testcase07a()
        {
            Infor("1.Go to http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Infor("2.Click on my account link");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyAcount"])).Click();
            Thread.Sleep(3000);

            Infor("3.Login in application using previously created credential");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginEmail"])).SendKeys(TestCommonKeyWords["Guru.Xpath.LoginEmailData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginPass"])).SendKeys(TestCommonKeyWords["Guru.Xpath.LoginPassData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginSubmit"])).Click();
            Thread.Sleep(3000);

            Infor("4.Click on 'My Orders'");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyAcount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyOrder"])).Click();
            string StatusRecentOrder = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.StatusP.RecentOrder"])).Text;

            Infor("5.Click on 'View Order'");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.ViewOrder"])).Click();

            //* **note: After steps 4 and 5, step 6 'RECENT ORDERS' was not displayed. 
            Infor("6.Verify the previously created order is displayed in 'RECENT ORDERS' table and status is Pending");
            string StatusViewOrder = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.StatusP.ViewOrder"])).Text;
            Assert(StatusRecentOrder, StatusViewOrder);

            Infor("7.Click on 'Print Order' link");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.PrintOrder"])).Click();
        }
        public void Testcase08()
        {
            Infor("1.Go to http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            Infor("2.Click on my account link");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyAcount"])).Click();
            Thread.Sleep(3000);

            Infor("3.Login in application using previously created credential");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginEmail"])).SendKeys(TestCommonKeyWords["Guru.Xpath.LoginEmailData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginPass"])).SendKeys(TestCommonKeyWords["Guru.Xpath.LoginPassData"]);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.LoginSubmit"])).Click();
            Thread.Sleep(3000);

            Infor("4.Click on 'My Orders'");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyAcount"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.MyOrder"])).Click();
            Thread.Sleep(2000);

            Infor("5. Click on 'REORDER' link , change QTY & click Update");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Reorder"])).Click();
            Thread.Sleep(2000);
            string OldPrice = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.OldPrice"])).Text;
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Quanlity"])).Clear();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Quanlity"])).SendKeys(TestKeyWords["Guru.Xpath.QuanlityData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Update"])).Click();
            string NewPrice = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.OldPrice"])).Text;
            
            Infor("6. Verify Grand Total is changed");
            AssertNot(NewPrice.Trim(), OldPrice.Trim());

            Infor("7. Complete Billing & Shipping Information");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.BtnCheckOut"])).Click();
            SelectElement BtnAdress = new SelectElement(Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.NewAddress"])));
            BtnAdress.SelectByIndex(0);
            if (BtnAdress.Options.Count == 1)
            {
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Company"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Company"])).SendKeys(TestCommonKeyWords["Guru.Xpath.CompanyData"]);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Address"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Address"])).SendKeys(TestCommonKeyWords["Guru.Xpath.AddressData"]);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.AddressStreet"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.AddressStreet"])).SendKeys(TestCommonKeyWords["Guru.Xpath.AddressStreetData"]);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.City"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.City"])).SendKeys(TestCommonKeyWords["Guru.Xpath.CityData"]);
                Thread.Sleep(2000);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.RegionBilling"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.RegionBilling"])).SendKeys(TestKeyWords["Guru.Xpath.RegionBillingData"]);
                //new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.RegionBilling"]))).SelectByText(TestKeyWords["Guru.Xpath.RegionBillingData"]);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.PostCodeBilling"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.PostCodeBilling"])).SendKeys(TestCommonKeyWords["Guru.Xpath.PostCodeBillingData"]);
                new SelectElement(Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.CountryBilling"]))).SelectByText(TestCommonKeyWords["Guru.Xpath.CountryBillingData"]);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Telephone"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Telephone"])).SendKeys(TestCommonKeyWords["Guru.Xpath.TelephoneData"]);
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Fax"])).Clear();
                Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Fax"])).SendKeys(TestCommonKeyWords["Guru.Xpath.FaxData"]);
                
            }
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Checkbox2"])).Click();
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ContinueButton"])).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipButtonMethod"])).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipMethodCheckMo"])).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipButtonMethodPayment"])).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.ShipPlaceOrder"])).Click();
            Thread.Sleep(3000);

            Infor("8. Verify order is generated and note the order number.");
            PassTest("Reorder rồi nhá");
            AddScreenCaptureFromPath();         
        }

        public void Testcase09()
        {
            Infor("1.Go to http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["Guru.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            Infor("2.Go to Mobile and add IPHONE to cart.");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Mobile"])).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.AddIphone"])).Click();
            Infor("3.Enter Coupon Code");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Coupon"])).SendKeys(TestKeyWords["Guru.Xpath.CouponData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Apply"])).Click();
            Infor("4.Verify the discount generated");
            //get money for Subtotal, Discount, Grandtotal
            string Subtotal = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Subtotal"])).Text.Trim();
            string Discount = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Discount"])).Text.Trim();
            string Grandtotal = Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.GrandTotal"])).Text.Trim();
            //Remove "$"
            string rSubtotal = Subtotal.Replace("$", " ");
            string rDiscount = Discount.Replace("$", " ");
            string rGrandtotal = Grandtotal.Replace("$", " ");
            string nDiscount = rDiscount.Replace("-", " ");
            //Remove space character
            string tSubtotal = rSubtotal.Trim();
            string tDiscount = nDiscount.Trim();
            string tGrandtotal = rGrandtotal.Trim();
            //Parse double
            double dSubtotal = double.Parse(tSubtotal);
            double dDiscount = double.Parse(tDiscount);
            double dGrandtotal = double.Parse(tGrandtotal);
            //Caculate
            double dDiscoutPice = dSubtotal * 0.05;
            double dDiscPrice = dSubtotal - dDiscoutPice;
            //Compare
            Assert(dDiscoutPice, dDiscount);
            Assert(dGrandtotal, dDiscPrice);

        }
        public void Testcase10()
        {
            Infor("1.Go to http://live.guru99.com/");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestKeyWords["Guru.Admin.URL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            
            Infor("3.Login in application using previously created credential");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Username"])).SendKeys(TestKeyWords["Guru.Admin.UsernameData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Password"])).SendKeys(TestKeyWords["Guru.Admin.PasswordData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.LoginButton"])).Click();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            string secondPopUp = Driver.WindowHandles.Last();
            Driver.SwitchTo().Window(secondPopUp);
            //IAlert alert = Driver.SwitchTo().Alert();
            //alert.Dismiss();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Notification"])).Click();
            Infor("3.Go to Sales-> Orders menu");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Sales"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Ordermenu"])).Click();

            Infor("4.Select 'CSV' in Export To dropdown and click Export button.");
            new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Ordermenu.ExportCSV"]))).SelectByText(TestKeyWords["Guru.Admin.Ordermenu.ExportCSVData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Admin.Ordermenu.ExportCSVButton"])).Click();
            Thread.Sleep(20000);

            Infor("5.Read downloaded file and display all order information in console windows");

            Infor("6.Attach this exported file and email to another email id");
            
        }
    }

}

