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
    class TC_1_02_01
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        WebDriverWait wait;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
        }

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234", wait);
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void Test_ProfileButtonClick_RightMenuOpened()
        {
            mainPageInstance.RightMenu.Open(wait);
            Assert.IsTrue(mainPageInstance.RightMenu.IsOpened());
        }

        [Test]
        public void Test_DropMouseFocus_RightMenuClosed()
        {
            Actions act = new Actions(driver);
            mainPageInstance.RightMenu.Open(wait);
            act.MoveToElement(mainPageInstance.ProfileButton)
                .MoveByOffset(-200, 200)
                .Build()
                .Perform();
            bool isRightMenuClosed = wait.Until((d) => !mainPageInstance.RightMenu.IsOpened());
            Assert.IsTrue(isRightMenuClosed);
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