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
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance.LogIn("artur", "1234");
            mainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPage.IsMainPage(d));
        }

        [Test]
        public void ExecuteTest_ExitButtonClicked_LoginPageOpened()
        {
            Acts.Click(mainPageInstance.ProfileButton);
            wait.Until(mainPageInstance.RightMenu.IsLogOutButtonClickable());
            Acts.Click(mainPageInstance.RightMenu.SignOutButton);
            Assert.IsTrue(wait.Until((d) => LoginPage.IsLoginPage(d)));
        }
        
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}