using AutomationTest.Models;
using Common.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace AutomationTest.Extensions
{
    public static class WebDriverExtension
    {
        public static void OpenFile(this IWebDriver webDriver, By by, string fileName)
        {
            var uploadElement = webDriver.FindElement(by);
            Thread.Sleep(2000);
            uploadElement.SendKeys(fileName);
        }
        public static bool IsExist(this IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.FindElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void GotoElement(this IWebDriver webDriver, By by)
        {
            var element = webDriver.FindElement(by);
            Actions actions = new Actions(webDriver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public static int ElementCount(this IWebDriver webDriver, By by)
        {
            try
            {
                return webDriver.FindElements(by).Count;
                
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static bool TryClickElement(this IWebDriver webDriver, By by)
        {
            try
            {
                webDriver.GotoElement(by);
                var element = webDriver.FindElement(by);
                if (!string.IsNullOrEmpty(element.GetAttribute("disabled")))
                {
                    return false;
                }
                webDriver.FindElement(by).Click();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void Wait(this IWebDriver webDriver, int miliseconds)
        {
            Thread.Sleep(miliseconds);
        }

        /// <summary>
        /// This method will return table information that is only used in ICC Template 
        /// </summary>
        /// <param name="recordsPerPage"></param>
        public static PagingInformation GetTableInformation(this IWebDriver webDriver, int recordsPerPage)
        {
            // tự thay thế mấy cái common text keywork vo mấy chỗ này
            if (webDriver.FindElement(By.CssSelector(".el-pagination")).GetCssValue("display") == "none")
            {
                return new PagingInformation()
                {
                    RecordsPerPage = recordsPerPage,
                    TotalPages = 1,
                    TotalRecords = webDriver.ElementCount(By.CssSelector(".el-table tr.el-table__row"))
                };
            }
            else
            {
                var result = new PagingInformation()
                {
                    RecordsPerPage = recordsPerPage,
                    TotalRecords = recordsPerPage,
                    TotalPages = 1
                };

                while (webDriver.TryClickElement(By.CssSelector(".el-pagination button.btn-next")))
                {
                    webDriver.Wait(1000);
                    result.TotalPages += 1;
                    result.TotalRecords += webDriver.ElementCount(By.CssSelector(".el-table tr.el-table__row"));
                }

                return result;
            }
        }
    }
}
