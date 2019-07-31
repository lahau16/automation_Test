﻿using Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Text;

namespace AutomationTest.Helpers
{
    public class DriverHelper
    {
        public static IWebDriver Create(DriverType type)
        {
            switch(type)
            {
                case DriverType.ChromeDriver:
                    return new ChromeDriver(Directory.GetCurrentDirectory());
                case DriverType.FireFoxDriver:
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    FirefoxOptions opt = new FirefoxOptions
                    {
                        Profile = new FirefoxProfile(Directory.GetCurrentDirectory())
                    };
                    var service = FirefoxDriverService.CreateDefaultService(Directory.GetCurrentDirectory(), "geckodriver.exe");
                    return new FirefoxDriver(service, opt, TimeSpan.FromMinutes(1));
                case DriverType.IEDriver:
                    InternetExplorerOptions opts = new InternetExplorerOptions()
                    {
                        IgnoreZoomLevel = true,
                        PageLoadStrategy = InternetExplorerPageLoadStrategy.Normal,
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true
                    };
                    var ieDriver = new InternetExplorerDriver(Directory.GetCurrentDirectory(), opts);
                    return ieDriver;
                default:
                    throw new Exception("Wrong driver type");
            }
        }
    }
}
