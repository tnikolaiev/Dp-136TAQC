using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_08_02
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;
        About aboutInstance;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
        }

        static Object[] GroupsOfContributors =
        {
            new String[] { "Team Doloto,Floppy-Drive 8,Fix Machine" },
        };

        [Test, TestCaseSource("GroupsOfContributors")]
        public void ExecuteTest_CheckListDevelopmentAndResearch(string expectedResult)
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.AboutItem);
            aboutInstance = new About(driver);
            Acts.Click(aboutInstance.DevelopmentResearch);
            List<string> actualResult = aboutInstance.GetTitleGroup();
            Console.WriteLine(actualResult);
            string[] listExpRes = expectedResult.Split(',');
            List<string> expectedRes = new List<string>(listExpRes);
            CollectionAssert.AreEqual(expectedRes, actualResult);


        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }

        [OneTimeTearDown]
        public void FinalCleanUp()
        {
            driver.Quit();
        }
    }
}

