using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CaesarLib;

namespace CaesarTests
{
    
    [TestFixture]
    public class TC_3_0_01
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [Test]
        public void AvailablaFunctionsTeacher()
        {

            bool expected = false;
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();
            bool actual = mainPageInstance.CenterContainer.GroupsContent.isCogwheelAvailable();
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void AvailablaFunctionsCoordinator()
        {

            bool expected = true;
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();
            bool actual = mainPageInstance.CenterContainer.GroupsContent.isCogwheelAvailable();
            Assert.AreEqual(expected, actual);

        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }

    }
}
