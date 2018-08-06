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
    class TC_1_04_03
    {
        IWebDriver driver;
        WebDriverWait wait;
        Actions action;
        LoginPage loginPageInstance;
        MainPage mainPageInstance;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [SetUp]
        public void Initialize()
        {           
            driver.Url = @"http://localhost:3000/logout";
            action = new Actions(driver);
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
        }

        [Test]
        public void Test_ClickCreateButton_GroupCreateWindowOpened()
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            
            Assert.IsTrue(groupCreateWindow.IsOpened());
        }

        [Test]
        public void Test_ClickCancelButton_GroupCreateWindowClosed()
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            groupCreateWindow.CancelGroupAddingButton.Click();
            bool isWindowClosed = wait.Until((d) => !groupCreateWindow.IsOpened());
            
            Assert.IsTrue(isWindowClosed);
        }

        [Test]
        public void Test_PressEscKey_GroupCreateWindowClosed()
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            action.SendKeys(Keys.Escape).Perform();
            wait.Until((d) => !groupCreateWindow.IsOpened());
            
            Assert.IsFalse(groupCreateWindow.IsOpened());
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