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
    public class TC_3_0_06
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
        public void FindingStatus()
        {
            string expected = "planned";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("09/10/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle.Click();

            string actual = mainPageInstance.CenterContainer.GroupStage.Text;
            Assert.AreEqual(expected,actual);
        }

        [Test]
        public void FindingStatus1()
        {
            string expected = "boarding";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("08/20/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();

            string actual = mainPageInstance.CenterContainer.GroupStage.Text;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindingStatus2()
        {
            string expected = "before-start";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("08/10/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.CurrentGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();

            string actual = mainPageInstance.CenterContainer.GroupStage.Text;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindingStatus3()
        {
            string expected = "in-process";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("08/08/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();

            string actual = mainPageInstance.CenterContainer.GroupStage.Text;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindingStatus4()
        {
            string expected = "offering";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("05/24/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();

            string actual = mainPageInstance.CenterContainer.GroupStage.Text;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindingStatus5()
        {
            string expected = "finished";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.StartDateField.SendKeys("05/01/2018");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();

            string actual = mainPageInstance.CenterContainer.GroupStage.Text;

            Assert.AreEqual(expected, actual);
        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
            driver.Quit();
        }
    }
}
