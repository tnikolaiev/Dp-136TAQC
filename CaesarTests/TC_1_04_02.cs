using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_02
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver;
        WebDriverWait wait;
        Actions action;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [SetUp]
        public void Initialize()
        {                     
            driver.Url = @"http://localhost:3000/logout";
            action = new Actions(driver);
        }

        static object[] CoordAdminCredentials = { new String[] { "dmytro", "1234" }, new String[] { "artur", "1234" } };

        [Test, TestCaseSource("CoordAdminCredentials")]
        public void ExecuteTest_SignInAsCoordinatorOrAdmin_TwoButtonsAvailable(String login, String password)
        {
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password, wait);
            mainPageInstance = new MainPage(driver);
            mainPageInstance.LeftMenu.Open(action, wait);

            List<String> expectedResult = new List<String> { "Create", "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            Log4Caesar.Log();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource("CoordAdminCredentials")]
        public void ExecuteTest_SignInAsCoordinatorOrAdmin_CheckGroup_FourButtonsAvailable(String login, String password)
        {
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password, wait);
            mainPageInstance = new MainPage(driver);
            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait);
            chosenGroup.Click();
            mainPageInstance.LeftMenu.Open(action, wait);

            List<String> expectedResult = new List<String> { "Create", "Search", "Edit", "Delete" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            Log4Caesar.Log();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        static object[] TeacherCredentials = { new String[] { "sasha", "1234" } };

        [Test, TestCaseSource("TeacherCredentials")]
        public void ExecuteTest_SignInAsTeacher_OneButtonsAvailable(String login, String password)
        {
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password, wait);
            mainPageInstance = new MainPage(driver);
            mainPageInstance.LeftMenu.Open(action, wait);

            List<String> expectedResult = new List<String> { "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            Log4Caesar.Log();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test, TestCaseSource("TeacherCredentials")]
        public void ExecuteTest_SignInAsTeacher_CheckGroup_OneButtonsAvailable(String login, String password)
        {
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn(login, password, wait);
            mainPageInstance = new MainPage(driver);
            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait);
            chosenGroup.Click();
            mainPageInstance.LeftMenu.Open(action, wait);

            List<String> expectedResult = new List<String> { "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            Log4Caesar.Log();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {            
            driver.Quit();
        }
    }
}
