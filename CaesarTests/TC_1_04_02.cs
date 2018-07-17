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

        [Test]
        public void ExecuteTest_SignInAsCoordinator_TwoButtonsAvailable()
        {
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Create", "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_SignInAsCoordinator_CheckGroup_FourButtonsAvailable()
        {
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);

            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            Acts.Click(chosenGroup);            

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Create", "Search", "Edit", "Delete" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_SignInAsTeacher_OneButtonsAvailable()
        {
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");

            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_SignInAsTeacher_CheckGroup_OneButtonsAvailable()
        {
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");

            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);

            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            Acts.Click(chosenGroup);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_SignInAsAdministrator_TwoButtonsAvailable()
        {
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("artur", "1234");

            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Create", "Search" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_SignInAsAdministrator_CheckGroup_FourButtonsAvailable()
        {
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("artur", "1234");

            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);

            IWebElement chosenGroup = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            Acts.Click(chosenGroup);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            List<String> expectedResult = new List<String> { "Create", "Search", "Edit", "Delete" };
            List<String> actualResult = mainPageInstance.LeftMenu.GetAvailableButtonsTitles();
            CollectionAssert.AreEqual(expectedResult, actualResult);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
