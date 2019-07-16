using AutomationTest.Models;
using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Admin.TestCases
{
    public class TestTrungNguyen : TestClass
    {
        public void Login()
        {
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = "http://27.71.232.114:40002"
            };
            Thread.Sleep(3000);

            //Login
            Driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/div[2]/div[1]/input")).SendKeys("admin");
            Driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/div[2]/div[2]/input")).SendKeys("Abcd1234");
            Driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/div[2]/div[3]/div/label/input")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath("/html/body/app-root/app-login/div/div/div/div[2]/div[4]/div[1]/button")).Click();
            Thread.Sleep(3000);
            Assert(Driver.Url, "http://27.71.232.114:40002/admin");
            Thread.Sleep(3000);
        }
        public void Logout()
        {
            
            Thread.Sleep(3000);
            //Logout
            Driver.FindElement(By.XPath("/html/body/app-root/app-root/mk-layout-wrapper/div/mk-layout-header/header/nav/app-header-inner/div/ul/li[2]/a")).Click();
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("/html/body/app-root/app-root/mk-layout-wrapper/div/mk-layout-header/header/nav/app-header-inner/div/ul/li[2]/ul/li[2]/div[2]/a")).Click();
            Thread.Sleep(3000);
            Assert(Driver.Url, "http://27.71.232.114:40002/authen/login");
        }
        public void Member()
        {
            Login();

            //Click Member
            Driver.FindElement(By.XPath("/html/body/app-root/app-root/mk-layout-wrapper/div/mk-layout-sidebar-left/aside/section/ul/li[3]/a/span")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath("//*[@id='grid']/div[1]/div/button")).Click();

            //Comment checkbox
            //Thread.Sleep(3000);
            //Driver.FindElement(By.XPath("//*[@id='grid']/div[2]/div[1]/div[1]/div/div[2]/div/input")).Click();
            Thread.Sleep(1000);
            //Write data in table
            int row = 1;
            while (true)
            {
                try
                {
                    Driver.FindElement(By.XPath($"//*[@id='grid']/div[2]/div[1]/div[2]/table/tbody/tr[{row}]/td[2]/input")).SendKeys($"Cô  {row}");
                    Driver.FindElement(By.XPath($"//*[@id='grid']/div[2]/div[1]/div[2]/table/tbody/tr[{row}]/td[3]/input")).SendKeys($"Nguyễn Ngọc Ngân  {row}");
                    Driver.FindElement(By.XPath($"//*[@id='grid']/div[2]/div[1]/div[2]/table/tbody/tr[{row}]/td[4]/input")).SendKeys($"Kế toán {row}");
                    row++;
                }
                catch
                {
                    break;
                }
            }
            //Update image Hình trang chủ
            var image1 = Driver.FindElement(By.XPath("//*[@id='grid']/div[2]/div[2]/div[1]/div[2]/div/img"));
            Actions builder = new Actions(Driver);
            builder.MoveToElement(image1).Build().Perform();
            Driver.FindElement(By.XPath("//*[@id='grid']/div[2]/div[2]/div[1]/div[2]/div/a/i")).Click();
            Thread.Sleep(3000);

            try
            {
                Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[1]/div[2]/div/div[1]/img")).Click();
            }
            catch
            {
                Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[2]/button")).Click();
                Thread.Sleep(10000);
                //System.Windows.Forms.SendKeys.SendWait(@"C:\Users\hoang\Desktop\embe.jpg");
                //System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
                Thread.Sleep(10000);
                Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[1]/div[2]/div/div[1]/img")).Click();

            }
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[3]/button[2]")).Click();
            Thread.Sleep(3000);

            //Update image Hình trang giới thiệu               
            var image2 = Driver.FindElement(By.XPath("//*[@id='grid']/div[2]/div[2]/div[2]/div[2]/div/img"));
            builder.MoveToElement(image2).Build().Perform();
            Driver.FindElement(By.XPath("//*[@id='grid']/div[2]/div[2]/div[2]/div[2]/div/a/i")).Click();
            Thread.Sleep(3000);
            try
            {
                Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[1]/div[2]/div/div[1]/img")).Click();
            }
            catch
            {
                Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[2]/button")).Click();
                Thread.Sleep(10000);
                //System.Windows.Forms.SendKeys.SendWait(@"C:\Users\hoang\Desktop\embe.jpg");
                //System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
                Thread.Sleep(10000);
                Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[1]/div[2]/div/div[1]/img")).Click();

            }
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//*[@id='open-gallery']/div[1]/div/div[3]/button[2]")).Click();
            Thread.Sleep(3000);

            //Click button Luu
            Driver.FindElement(By.XPath("//*[@id='grid']/div[1]/div/button[2]")).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("/html/body/div/div/div[3]/button[1]")).Click();
            Logout();
           
            //Driver.FindElement(By.XPath(TestKeyWords["Xpath.LinkManageAcount"])).Click();
            //Thread.Sleep(10000);
            //Assert(Driver.Url, TestKeyWords["UrlProfile"]);
            //Driver.FindElement(By.XPath(TestKeyWords["Xpath.LinkEdit"])).Click();
            //Thread.Sleep(3000);

           
        }
        public void ImageStored()
        {
            Login();
            Driver.FindElement(By.XPath("/html/body/app-root/app-root/mk-layout-wrapper/div/mk-layout-sidebar-left/aside/section/ul/li[11]/a/span")).Click();
            Thread.Sleep(3000);
            Driver.FindElement(By.XPath("//*[@id='grid']/div/div[2]/div[1]/div[2]/div/button[1]")).Click();
            Thread.Sleep(10000);
            //System.Windows.Forms.SendKeys.SendWait(@"C:\Users\hoang\Desktop\embe.jpg");
            //System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
            Thread.Sleep(1000);

            var category = Driver.FindElement(By.XPath("//*[@id='grid']/div/div[1]/div/div[2]/div[1]/div/select"));
            //create select element object 
            var selectElement = new SelectElement(category);

            //select by value
            selectElement.SelectByIndex(0);
            // select by text
            selectElement.SelectByText("Member");
            Thread.Sleep(10000);

        }
       
    }
}
