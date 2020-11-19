using System;
using System.Threading;
using System.Net;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace webrequest
{
    class Program
    {
        //create reference of our web
        IWebDriver driver = new ChromeDriver();
   
        static void Main(string[] args)
        {
            //Program obj = new Program();
            MethodsClass obj = new MethodsClass();
            obj.initilize();
            obj.loginTestCases();
            //obj.DashboardPageLinks();
            //obj.challengePageLinks();
            //obj.TransactionPageLinks();
            //obj.UserPageLinks();
            //obj.CategoryPageLinks();
            //obj.NotificationPageLinks();
            //obj.CustomMessagePageLinks();
            //obj.SettingPageLinks();
            //obj.webrequestgetMethod();
            //obj.transactionDetail();
            //obj.alluserlink();
            obj.AllchallengeandDescriptionLinks();
            //obj.challengeDescriptionPageLinks();
        }

    }
}
