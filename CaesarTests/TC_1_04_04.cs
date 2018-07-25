using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using OpenQA.Selenium;

namespace CaesarLib
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
            action = new Actions(driver);
        }

        [Test]
        public void ExecuteTest_SelectGroup_ClickDeleteButton_GroupDeleteConfirmantionWindowOpened()
        {
            var groupDeleteConfirmantionWindow = mainPageInstance.ModalWindow.GroupDeleteConfirmationWindow;
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait).Click();
            mainPageInstance.LeftMenu.Open(action, wait);
            mainPageInstance.LeftMenu.DeleteButton.Click();
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
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS", wait).Click();
            mainPageInstance.LeftMenu.Open(action, wait);
            mainPageInstance.LeftMenu.DeleteButton.Click();
            wait.Until((d) => groupDeleteConfirmantionWindow.IsOpened());
            action.SendKeys(Keys.Escape).Perform();
            wait.Until((d) => !groupDeleteConfirmantionWindow.IsOpened());
            Assert.IsFalse(groupDeleteConfirmantionWindow.IsOpened());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}