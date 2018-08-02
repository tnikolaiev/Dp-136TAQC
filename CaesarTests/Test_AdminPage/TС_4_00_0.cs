using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    public class TС_4_00_0
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        AdminPage adminPage;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("Dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            driver.Url = @"http://localhost:3000/admin";
            wait.Until((d) => AdminPage.IsAdminPageOpened(d));
            adminPage = new AdminPage(driver);
        }

        [Test]
        public void Test_CreateUserFormIsDisplayed()
        {
            adminPage.EscapeHome.Click();

            Assert.IsTrue(wait.Until((d) => MainPage.IsMainPageOpened(d)));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
