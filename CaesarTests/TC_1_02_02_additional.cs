using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    public class TC_1_02_02_additional
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
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
        }
        
        [Test]
        public void ExecuteTest_LogoutFromTopMenu()
        {
            mainPageInstance.LogoutUsingTopMenu();
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