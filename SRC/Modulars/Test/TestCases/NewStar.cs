using AutoItX3Lib;
using AutomationTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Test.TestCases
{
    public class NewStar : TestClass
    {
        public void Login()
        {
            Infor("Truy cập vào link http://newstarmkt.com/authen/login");
            Driver = new ChromeDriver(Directory.GetCurrentDirectory())
            {
                Url = TestCommonKeyWords["BaseURL"]
            };
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            Infor("1. Nhập username");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.Xpath.Username"])).SendKeys(TestKeyWords["Newstar.Xpath.UsernameData"]);
                       
            Infor("2. Nhập password");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.Xpath.Password"])).SendKeys(TestKeyWords["Newstar.Xpath.PasswordData"]);

            Infor("3. Tích ô checkbox");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.Xpath.Checkbox"])).Click();

            Infor("4. Bấm vào nút Login");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.Xpath.LoginButton"])).Click();
            Thread.Sleep(3000);
            Assert(TestKeyWords["Newstar.Compare.Loginadmin"].Trim(), Driver.Url.Trim());

            Thread.Sleep(3000);
        }
        public void Logout()
        {
            Thread.Sleep(3000);
            Infor("1. Trên thanh công cụ, bấm Administrator");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.Logout.Xpath.Adminitrator"])).Click();
            Thread.Sleep(3000);
            Infor("2. Bấm vào nút Logout");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.Logout.Xpath.LogoutButton"])).Click();
            Thread.Sleep(3000);
            Assert(TestCommonKeyWords["BaseURL"].Trim(), Driver.Url.Trim());
            Thread.Sleep(3000);
        }

        public void UpdateAvartar()
        {
            Login();
            Infor("Đi đến trang Người dùng");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.UpdateAvatar.HeThong"])).Click();
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.UpdateAvatar.NguoiDung"])).Click();
            Thread.Sleep(1000);
            Infor("Tìm kiếm người dùng muốn thay đổi");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.UpdateAvatar.Search"])).SendKeys(TestKeyWords["Newstar.UpdateAvatar.SearchData"]);
            Thread.Sleep(2000);
            Infor("Bắt đầu đến màn hình chỉnh sửa thông tin người đã chọn");
            Driver.FindElement(By.XPath(TestKeyWords["Newstar.UpdateAvatar.EditButton"])).Click();
            Thread.Sleep(3000);
            AddScreenCaptureFromPath();
            Infor("Update lại avartar");
            var avatar = Driver.FindElement(By.XPath(TestKeyWords["Newstar.UpdateAvatar.Avatar"]));
            avatar.Click();
            Thread.Sleep(10000);
            AutoItX3 autoIT = new AutoItX3();

            // đưa title của cửa sổ File upload vào chuỗi. 
            // Cửa sổ hiện ra có thể có tiêu đề là File Upload hoặc Tải lên một tập tin
            // lấy ra cửa sổ active có tiêu đề như dưới
            autoIT.WinActivate("Open");

            // file data nằm trong thư mục debug
            // gửi link vào ô đường dẫn
            autoIT.Send(TestKeyWords["Newstar.UpdateAvatar.AvatarFile"]);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            // gửi phím enter sau khi truyền link vào
            autoIT.Send("{ENTER}");
            Thread.Sleep(5000);
            AddScreenCaptureFromPath();
            Thread.Sleep(3000);
            Logout();

        }
    }
}
