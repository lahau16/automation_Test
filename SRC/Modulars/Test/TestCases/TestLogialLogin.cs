using AutomationTest.Helpers;
using AutomationTest.Models;
using System.Dynamic;
using Test.UI.Models;

namespace Test.TestCases
{
    public class TestLogialLogin : TestClass
    {
        public void Login()
        {
            var username = "";
            var pasword = "";
            var baseurl = "";
            var route = "";
            dynamic dataLogin = new ExpandoObject();
            dataLogin.userName = username;
            dataLogin.password = pasword;
            var user = WebHelper.HttpPost<UserSession>(TestKeyWords["BaseUrl"], route, (object)dataLogin).Result;
            AssertNot(user, null);
        }

        public void Logout()
        {
            var username = "";
            var pasword = "";
            var baseurl = "";
            var route = "";
            var routelogout = "";
            dynamic dataLogin = new ExpandoObject();
            dataLogin.userName = username;
            dataLogin.password = pasword;
            var user = WebHelper.HttpPost<UserSession>(baseurl, route, (object)dataLogin).Result;

            AssertNot(user, null);

            var logout = WebHelper.HttpPost<UserSession>(baseurl, routelogout, user.AccessToken).Result;
            AssertNot(logout, null);
        }

        public void SalesPlanNotPermission()
        {
            var username = "";
            var pasword = "";
            var baseurl = "";
            var route = "";
            var routeSalesPlan = "";
            dynamic dataLogin = new ExpandoObject();
            dataLogin.userName = username;
            dataLogin.password = pasword;
            var user = WebHelper.HttpPost<UserSession>(baseurl, route, (object)dataLogin).Result;

            AssertNot(user, null);

            var logout = WebHelper.HttpGet<UserSession>(baseurl, routeSalesPlan + "?from=" , user.AccessToken).Result;
            AssertNot(logout, null);
        }
    } 
}
