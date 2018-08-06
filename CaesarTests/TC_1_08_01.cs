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
    class TC_1_08_01
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
            new String[] { "Development & Research,Quality Assurance,Management and Mentoring,Additional Thanks" },
        };

        [Test, TestCaseSource("GroupsOfContributors")]
        public void ExecuteTest_CheckButtonAbout(string expectedResult)
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.AboutItem);
            aboutInstance = new About(driver);
            List<String> actualResult = aboutInstance.GetButtonsName(wait);
            string lineActualResult = (string.Join(",", actualResult.ToArray()));
            Console.WriteLine(lineActualResult);
            Assert.AreEqual(expectedResult, lineActualResult);
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

