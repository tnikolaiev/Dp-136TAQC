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
        }

        [SetUp]
        public void Initialize()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Manage().Window.Maximize();
            driver.Url = @"http://localhost:3000/logout";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
            action = new Actions(driver);
        }

        [Test]
        public void ExecuteTest_SelectGroup_ClickDeleteButton_GroupDeleteConfirmantionWindowOpened()
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
        public void ExecuteTest_ClickCancelButton_GroupDeleteConfirmantionWindowClosed()
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
        public void ExecuteTest_PressEscKey_GroupDeleteConfirmantionWindowOpenedClosed()
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

        [OneTimeTearDown]
        public void CleanUp()
        {            
            driver.Quit();
        }
    }
}