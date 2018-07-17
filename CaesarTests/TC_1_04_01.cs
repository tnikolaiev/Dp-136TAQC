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
    class TC_1_04_01
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");
            mainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPage.IsMainPage(d));
        }

        [Test]
        public void ExecuteTest_CursorToLeftBorder_LeftMenuOpened()
        {
            Actions acts = new Actions(driver);
            mainPageInstance.LeftMenu.Open(acts);
            Assert.IsTrue(wait.Until((d) => mainPageInstance.LeftMenu.IsOpened()));
        }

        [Test]
        public void ExecuteTest_CursorFocusOutOfMenu_LeftMenuClosed()
        {
            Actions acts = new Actions(driver);
            mainPageInstance.LeftMenu.Open(acts);
            acts.MoveByOffset(300, 300).Perform();
            wait.Until((d) => !mainPageInstance.LeftMenu.IsOpened());
            Assert.IsFalse(mainPageInstance.LeftMenu.IsOpened());

        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
