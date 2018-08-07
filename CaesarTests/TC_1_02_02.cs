using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_02_02
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
            loginPageInstance.LogIn("artur", "1234", wait);
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void Test_ExitButtonClicked_LoginPageOpened()
        {
            mainPageInstance.RightMenu.Open(wait);
            mainPageInstance.RightMenu.SignOutButton.Click();
            Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPageOpened(d)));
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