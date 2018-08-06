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
    public class TC_3_0_03
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;
        MainPage mainPageInstance1;

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
            mainPageInstance1 = new MainPage(driver);
        }

        [Test]
        public void EditingGroupName()
        {
            string expected1 = "DP-000-EX";
            int expected2 = 0;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-027-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.Clear();
            mainPageInstance.ModalWindow.GroupCreateWindow.GroupNameField.SendKeys("DP-000-EX");
            mainPageInstance.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-000-EX").Click();
            string actual1 = mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-000-EX").Text;
            mainPageInstance.CenterContainer.GroupsContent.StudentsTab.Click();
            int actual2 = 0;
            foreach (var item in mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetRows())
            {
                if (item.Text != "")
                    actual2++;
            }

            Assert.IsTrue((expected1 == actual1) & (expected2 == actual2));

            

        }

        [TearDown]
        public void CleanUp()
        {
            mainPageInstance1.CenterContainer.GroupsContent.InfoTab.Click();
            mainPageInstance1.CenterContainer.GroupsContent.CogWheel.Click();
            mainPageInstance1.ModalWindow.GroupCreateWindow.GroupNameField.Clear();
            mainPageInstance1.ModalWindow.GroupCreateWindow.GroupNameField.SendKeys("DP-027-JS");
            mainPageInstance1.ModalWindow.GroupCreateWindow.SaveGroupButton.Click();
            driver.Quit();
        }

    }
}
