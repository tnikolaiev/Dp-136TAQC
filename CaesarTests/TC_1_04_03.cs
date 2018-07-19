using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_03
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
            driver.Url = @"http://localhost:3000/logout";
        }

        [Test]
        public void ExecuteTest_SignInAsCoordinator_OpenCreateGroupWindow_DirectionDDlnotEnabled()
        {
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            mainPageInstance.LeftMenu.CreateButton.Click();
            bool createGroupWindowOpened = wait.Until((d) => mainPageInstance.ModalWindow.CreateGroupWindow.IsCreateGroupWindowOpened());

            bool LocationDdlEnabled = mainPageInstance.ModalWindow.CreateGroupWindow.LocationDDL.Enabled;
            Assert.IsTrue(createGroupWindowOpened & !LocationDdlEnabled);
        }

        [Test]
        public void ExecuteTest_OpenCreateGroupWindow_PressCancelButton_CreateGroupWindowClosed()
        {
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234");

            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

            mainPageInstance.LeftMenu.Open(new Actions(driver));
            wait.Until(mainPageInstance.LeftMenu.IsSearchButtonVisible());

            mainPageInstance.LeftMenu.CreateButton.Click();
            wait.Until((d) => mainPageInstance.ModalWindow.CreateGroupWindow.IsCreateGroupWindowOpened());

            mainPageInstance.ModalWindow.CreateGroupWindow.CancelGroupAddingButton.Click();
            wait.Until((d) => !mainPageInstance.ModalWindow.CreateGroupWindow.IsCreateGroupWindowOpened());
            Assert.IsFalse(mainPageInstance.ModalWindow.CreateGroupWindow.IsCreateGroupWindowOpened());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
