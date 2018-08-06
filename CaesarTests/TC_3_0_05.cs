using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CaesarLib;

namespace CaesarTests
{

    [TestFixture]
    public class TC_3_0_05
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;

        [SetUp]
        public void Initialize()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

        }

        [Test]
        public void EditingDate()
        {
            List<string> expected = new List<string>();
            expected.Add("02/02/2017");
            expected.Add("04/26/2017");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("02/02/2017");
            mainPageInstance.ModalWindow.GroupCreateWindow.FinishDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.FinishDateField.SendKeys("04/26/2017");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();

            List<string> actual = new List<string>();
            actual.Add(mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupInfo.GetValueFromCell(2, 2));
            actual.Add(mainPageInstance.CenterContainer.GroupsContent.InfoPage.GroupInfo.GetValueFromCell(3, 2));
            CollectionAssert.AreEqual(expected,actual);

        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
            driver.Quit();
        }


    }
}
