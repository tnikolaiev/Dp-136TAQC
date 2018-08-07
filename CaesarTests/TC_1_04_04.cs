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
    class TC_1_04_04
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);            
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [SetUp]
        public void Initialize()
        {
            
            driver.Url = @"http://localhost:3000/logout";
           
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
            action = new Actions(driver);
        }

        [Test]
        public void Test_SelectGroup_ClickDeleteButton_GroupDeleteConfirmantionWindowOpened()
        {
            var groupDeleteConfirmantionWindow = mainPageInstance.ModalWindow.GroupDeleteConfirmationWindow;
            var leftMenu = mainPageInstance.LeftMenu;
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait).Click();
            leftMenu.Open(action, wait);            
            wait.Until((d) => leftMenu.IsDeleteButtonVisible());
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", leftMenu.DeleteButton);
            
            Assert.IsTrue(wait.Until((d) => groupDeleteConfirmantionWindow.IsOpened()));
        }

        [Test]
        public void Test_ClickCancelButton_GroupDeleteConfirmantionWindowClosed()
        {
            var groupDeleteConfirmantionWindow = mainPageInstance.ModalWindow.GroupDeleteConfirmationWindow;
            var leftMenu = mainPageInstance.LeftMenu;
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait).Click();
            leftMenu.Open(action, wait);
            wait.Until((d) => leftMenu.IsDeleteButtonVisible());
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", leftMenu.DeleteButton);
            wait.Until((d) => groupDeleteConfirmantionWindow.IsCancelButtonVisible());
            groupDeleteConfirmantionWindow.CancelButton.Click();
            bool isGroupDeleteConfirmationWindowClosed = wait.Until((d) => !groupDeleteConfirmantionWindow.IsOpened());
            
            Assert.IsTrue(isGroupDeleteConfirmationWindowClosed);
        }

        [Test]
        public void Test_PressEscKey_GroupDeleteConfirmantionWindowOpenedClosed()
        {
            var groupDeleteConfirmantionWindow = mainPageInstance.ModalWindow.GroupDeleteConfirmationWindow;
            var leftMenu = mainPageInstance.LeftMenu;
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait).Click();
            leftMenu.Open(action, wait);
            wait.Until((d) => leftMenu.IsDeleteButtonVisible());
            (driver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", leftMenu.DeleteButton);
            wait.Until((d) => groupDeleteConfirmantionWindow.IsOpened());
            action.SendKeys(Keys.Escape).Perform();
            bool isGroupDeleteConfirmationWindowClosed = wait.Until((d) => !groupDeleteConfirmantionWindow.IsOpened());
            
            Assert.IsTrue(isGroupDeleteConfirmationWindowClosed);
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