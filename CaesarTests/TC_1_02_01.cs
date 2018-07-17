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
    class TC_1_02_01
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPage(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPage(d));
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void ExecuteTest_ProfileButtonClick_RightMenuOpened()
        {
            Acts.Click(mainPageInstance.ProfileButton);
            Assert.IsTrue(wait.Until((d) => mainPageInstance.RightMenu.IsOpened()));
        }

        [Test]
        public void Executetest_DropMouseFocus_RightMenuClosed()
        {
            Actions act = new Actions(driver);
            Acts.Click(mainPageInstance.ProfileButton);
            wait.Until((d) => mainPageInstance.RightMenu.IsOpened());
            act.MoveToElement(mainPageInstance.ProfileButton)
                .MoveByOffset(-200, 200)
                .Build()
                .Perform();
            wait.Until((d) => !mainPageInstance.RightMenu.IsOpened());
            Assert.IsFalse(mainPageInstance.RightMenu.IsOpened());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}