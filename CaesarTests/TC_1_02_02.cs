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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("artur", "1234");
            mainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPage.IsMainPageOpened(d));
        }

        [Test]
        public void ExecuteTest_ExitButtonClicked_LoginPageOpened()
        {
            mainPageInstance.ProfileButton.Click();
            wait.Until(mainPageInstance.RightMenu.IsLogOutButtonClickable());
            mainPageInstance.RightMenu.SignOutButton.Click();
            Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPageOpened(d)));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}