using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_01
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver;
        WebDriverWait wait;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
        }

        [SetUp]
        public void Initialize()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234", wait);
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void Test_CursorToLeftBorder_LeftMenuOpened()
        {
            Actions acts = new Actions(driver);
            mainPageInstance.LeftMenu.Open(acts, wait);
            Assert.IsTrue(wait.Until((d) => mainPageInstance.LeftMenu.IsOpened()));
        }

        [Test]
        public void Test_CursorFocusOutOfMenu_LeftMenuClosed()
        {
            Actions acts = new Actions(driver);
            mainPageInstance.LeftMenu.Open(acts, wait);
            acts.MoveByOffset(300, 300).Perform();
            bool isLeftMenuClosed = wait.Until((d) => !mainPageInstance.LeftMenu.IsOpened());
            Assert.IsTrue(isLeftMenuClosed);
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
