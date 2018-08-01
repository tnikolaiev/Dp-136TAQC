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
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;
        Actions action;
        LoginPage loginPageInstance;
        MainPage mainPageInstance;

        [SetUp]
        public void Initialize()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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
            //
            //action = new Actions(driver);
            //action.ClickAndHold(leftMenu.DeleteButton).Release().Build().Perform();

            wait.Until((d) => leftMenu.IsDeleteButtonVisible());
            do leftMenu.DeleteButton.Click();
            while (!wait.Until((d) => groupDeleteConfirmantionWindow.IsOpened()));
            Assert.IsTrue(wait.Until((d) => groupDeleteConfirmantionWindow.IsOpened()));
        }

        [Test]
        public void ExecuteTest_ClickCancelButton_GroupDeleteConfirmantionWindowClosed()
        {
            var groupDeleteConfirmantionWindow = mainPageInstance.ModalWindow.GroupDeleteConfirmationWindow;
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait).Click();
            mainPageInstance.LeftMenu.Open(action, wait);
            mainPageInstance.LeftMenu.DeleteButton.Click();
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
            //
            //action = new Actions(driver);            
            //action.ClickAndHold(leftMenu.DeleteButton).Release().Build().Perform();

            wait.Until((d) => leftMenu.IsDeleteButtonVisible());
            do leftMenu.DeleteButton.Click();
            while (!wait.Until((d) => groupDeleteConfirmantionWindow.IsOpened()));
            action.SendKeys(Keys.Escape).Perform();
            bool isGroupDeleteConfirmationWindowClosed = wait.Until((d) => !groupDeleteConfirmantionWindow.IsOpened());
            Assert.IsTrue(isGroupDeleteConfirmationWindowClosed);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}