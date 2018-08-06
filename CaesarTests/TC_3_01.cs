using System;
using System.Text;
using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using CaesarLib;

namespace CaesarTests
{
    
    [TestFixture]
    public class TC_3_01
    {
        IWebDriver driver;
        LoginPage loginPageInstance;
        WebDriverWait wait;
        MainPage mainPageInstance;
        Table table;

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
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

        }

        [Test]
        public void ReviewingStudents1()
        {
            int expected = 4;
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.StudentsTab.Click();
            int actual = 0;
            foreach (var item in mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetRows())
            {
                if (item.Text != "")
                    actual++;
            }
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void ReviewingStudents2()
        {
            int expected = 0;
            mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-092-NET").Click();
            mainPageInstance.CenterContainer.GroupsContent.StudentsTab.Click();

            int actual = 0;
            foreach (var item in mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetRows())
            {
                if (item.Text != "")
                    actual++;
            }

            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void ReviewingStudents3()
        {
            int expected = 0;
            mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle.Click();
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-095-JS").Click();
            mainPageInstance.CenterContainer.GroupsContent.StudentsTab.Click();

            int actual = 0;
            foreach (var item in mainPageInstance.CenterContainer.StudentsContent.StudentTable.GetRows())
            {
                if (item.Text != "")
                    actual++;
            }

            Assert.AreEqual(expected, actual);

        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
