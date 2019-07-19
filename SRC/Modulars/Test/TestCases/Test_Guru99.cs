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
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.LoginEmail"])).SendKeys(TestKeyWords["Guru.Xpath.LoginEmailData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.LoginPass"])).SendKeys(TestKeyWords["Guru.Xpath.LoginPassData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.MyAcount"])).Click();

            Infor("4.Click on MY WISHLIST link");
            Driver.FindElement(By.XPath(TestCommonKeyWords["Guru.Xpath.Acount"])).Click();
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.MyWhistlist"])).Click();

            Infor("5.In next page, Click ADD TO CART link");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.AddToCartWhistlist"])).Click();

            Infor("6.Enter general shipping country, state / province and zip for the shipping cost estimate");
            new SelectElement(Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Country"]))).SelectByValue("VietNam");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Region"])).SendKeys(TestKeyWords["Guru.Xpath.RegionData"]);
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.PostCode"])).SendKeys(TestKeyWords["Guru.Xpath.PostCodeData"]);

            Infor("7.Click Estimate");
            Driver.FindElement(By.XPath(TestKeyWords["Guru.Xpath.Estimate"])).Click();

            Infor("8.Verify Shipping cost generated");

            Infor("9.Select Shipping Cost, Update Total");
            Infor("10.Verify shipping cost is added to total");
            Infor("11.Click 'Proceed to Checkout'");
            Infor("12a.Enter Billing Information, and click Continue");
            Infor("12b.Enter Shipping Information, and click Continue");
            Infor("13.In Shipping Method, Click Continue");
            Infor("14.In Payment Information select 'Check/Money Order' radio button.Click Continue");
            Infor("15.Click 'PLACE ORDER' button");
            Infor("16.Verify Oder is generated.Note the order number");

            //NOTE: PROCEED TO CHECKOUT (step 6 ) was taken out, so as to allow the Estimate button step to get processed. 
            //Rest of the steps renumbered accordingly.
        }
        public void Testcase07a()
        {
            Infor("1. Go to http://live.guru99.com/");
            Infor("2.Click on My Account link");
            Infor("3.Login in application using previously created credential");
            Infor("4.Click on 'My Orders'");
            Infor("5.Click on 'View Order'");
            //* **note: After steps 4 and 5, step 6 'RECENT ORDERS' was not displayed. 
            Infor("6.Verify the previously created order is displayed in 'RECENT ORDERS' table and status is Pending");
            Infor("7.Click on 'Print Order' link");
            // * **note: the link was not found. 
            Infor("8.Click 'Change...' link and a popup will be opened as 'Select a destination', select 'Save as PDF' link.");
            //* **unable to execute this step, due to issue with step 8 issue.
            Infor("9.Click on 'Save' button and save the file in some location.");
            //***unable to execute this step, due to steps 8 and 9 issues.
            Infor("10.Verify Order is saved as PDF");
        }
        public void Testcase07b()
        {
            Infor("1. Go to http://live.guru99.com/");
            Infor("2. Click on My Account link");
            Infor("3. Login in application using previously created credential ");
            Infor("4. Click on 'My Orders'");
            Infor("5. Click on 'View Order'");
            //*** when steps 4 and 5 are executed, step 6 RECENT ORDERS was not displayed
            Infor("6. Verify the previously created order is displayed in 'RECENT ORDERS' table and status is Pending");
            Infor("7. Click on 'Print Order' link");
            //*** note: the Change ... link was not found.
            Infor("8. Click 'Change...' link and a popup will be opened as 'Select a destination' , select 'Save as PDF' link.");
            //*** unable to execute this step, due to issue with step 8 issue
            Infor("9. Click on 'Save' button and save the file in some location.");
            //***unable to execute this step, due to steps 8 and 9 issues.
            Infor("10. Verify Order is saved as PDF");
            /*Expected results:
            1. Previously created order is displayed in 'RECENT ORDERS' table and status is Pending.
            2. Order is saved as PDF.*/

        }
    }
}

