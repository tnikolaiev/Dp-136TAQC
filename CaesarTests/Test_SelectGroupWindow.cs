using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class Test_SelectGroupWindow
    {
        IWebDriver webDriver = new ChromeDriver();
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            //SetUp WebDriver
            webDriver.Manage().Window.Maximize();
            wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            //Open LoginPage
            webDriver.Url = baseURL;
            wait.Until((driver) => LoginPage.IsLoginPageOpened(driver));
            loginPageInstance = new LoginPage(webDriver);
            //Login as Teacher
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((driver) => MainPage.IsMainPageOpened(driver));
            mainPageInstance = new MainPage(webDriver);
            //Go to Schedule Page
            webDriver.Url = baseURL + "/Schedule/Dnipro";
            wait.Until((driver) => MainPage.IsMainPageOpened(driver));
        }
        [Test]
        public void ExecuteTest_1_SelectTwoGroups_SelectGroupWindowOpened()
        {
            List<string> expected = new List<string> { "DP-093-JS", "DP-094-MQC" };
            //Select two groups from LeftContainer
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
            //Click cogwheel button to edit schedule content -> SelectGroupWindow is opened
            mainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();
            wait.Until((driver) => mainPageInstance.ModalWindow.SelectGroupWindow.IsSelectGroupWindowOpened(driver));
            CollectionAssert.AreEquivalent(expected, mainPageInstance.ModalWindow.SelectGroupWindow.GetSelectedGroupNames());
        }
        [Test]
        public void ExecuteTest_2_SelectGroup_EditScheduleWindowForSelectedGroupIsOpened()
        {
            //Select group "DP-093-JS"
            mainPageInstance.ModalWindow.SelectGroupWindow.GetGroupByName("DP-093-JS").Click();
            mainPageInstance.ModalWindow.SelectGroupWindow.SaveButton.Click();
            Assert.IsTrue(mainPageInstance.ModalWindow.EditScheduleWindow.IsScheduleEditorDisplayed(webDriver));
        }
        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }
        [OneTimeTearDown]
        public void OneTimeTearDownTest()
        {
            webDriver.Close();
            webDriver.Quit();
        }
    }
}
