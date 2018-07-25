﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using OpenQA.Selenium;

namespace CaesarLib
{
    [TestFixture]
    class TC_1_04_03
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
            action = new Actions(driver);                    
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);           
        }

        [Test]
        public void ExecuteTest_ClickCreateButton_GroupCreateWindowOpened()
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            Assert.IsTrue(groupCreateWindow.IsOpened());
        }

        [Test]
        public void ExecuteTest_ClickCancelButton_GroupCreateWindowClosed()
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            groupCreateWindow.CancelGroupAddingButton.Click();
            bool isWindowClosed = wait.Until((d) => !groupCreateWindow.IsOpened());
            Assert.IsTrue(isWindowClosed);
        }

        [Test]
        public void ExecuteTest_PressEscKey_GroupCreateWindowClosed()
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            action.SendKeys(Keys.Escape).Perform();
            wait.Until((d) => !groupCreateWindow.IsOpened());
            Assert.IsFalse(groupCreateWindow.IsOpened());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}