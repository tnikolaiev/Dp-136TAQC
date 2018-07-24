using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_02
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
        }

        static object[] CoordAdminCredentials =
            {
            new String[] { "dmytro", "1234" },
            new String[] { "artur", "1234" }
        };

        [Test, TestCaseSource("CoordAdminCredentials")]
        public void ExecuteTest_SignInAsCoordinatorOrAdmin_TwoButtonsAvailable(String login, String password)
        {
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password);

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Create", "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource("CoordAdminCredentials")]
        public void ExecuteTest_SignInAsCoordinatorOrAdmin_CheckGroup_FourButtonsAvailable(String login, String password)
        {
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password);

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            chosenGroup.Click();

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Create", "Search", "Edit", "Delete" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        static object[] TeacherCredentials =
            {
            new String[] { "sasha", "1234" }
        };

        [Test, TestCaseSource("TeacherCredentials")]
        public void ExecuteTest_SignInAsTeacher_OneButtonsAvailable(String login, String password)
        {
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password);

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource("TeacherCredentials")]
        public void ExecuteTest_SignInAsTeacher_CheckGroup_OneButtonsAvailable(String login, String password)
        {
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password);

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            chosenGroup.Click();

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
