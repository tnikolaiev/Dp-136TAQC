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
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("artur", "1234", wait);
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void ExecuteTest_ExitButtonClicked_LoginPageOpened()
        {
            mainPageInstance.RightMenu.Open(wait);
            mainPageInstance.RightMenu.SignOutButton.Click();
            Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPageOpened(d)));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}