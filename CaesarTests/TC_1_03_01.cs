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
    class TC_1_03_01
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;
        GroupsInLocation groupsList;

        [SetUp]
        public void Initialize()
        {
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("admin", "1234", wait);
            mainPageInstance = new MainPage(driver);
            groupsList = mainPageInstance.LeftContainer.GroupsInLocation;
            wait.Until((d) => groupsList.AreTogglesVisible());
        }

        [Test]
        public void ExecuteTest_FirstSignIn_AvailableGroupsList()
        {
            List<String> expectedResult = new List<String> { "Lv-084-QB", "Lv-045-DL", "Lv-023-UX" };
            wait.Until((d) => groupsList.AreGroupsVisible());
            List<String> actualResult = groupsList.GetAvailableGroupsNames(wait);
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_MyGroupsFilterCheckedUnchecked()
        {
            mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter.Click();
            bool isMyGroupsFilterChecked = "myGroups pressed".Equals(groupsList.MyGroupsFilter.GetAttribute("class"));
            groupsList.MyGroupsFilter.Click();
            bool isMyGroupFilterUnckecked = "myGroups".Equals(groupsList.MyGroupsFilter.GetAttribute("class"));
            Assert.IsTrue(isMyGroupsFilterChecked && isMyGroupFilterUnckecked);
        }

        [Test]
        public void ExecuteTest_MyGroupsFilterChecked_NoneAvailableGroups()
        {
            groupsList.MyGroupsFilter.Click();
            List<String> actualResult = groupsList.GetAvailableGroupsNames(wait);
            Assert.IsEmpty(actualResult);
        }

        [Test]
        public void ExecuteTest_FutureGroupsFilter()
        {
            groupsList.FutureGroupsToggle.Click();
            List<String> actualResult = groupsList.GetAvailableGroupsNames(wait);
            Assert.IsEmpty(actualResult);
        }

        [Test]
        public void ExecuteTest_EndedGroupsFilter()
        {
            groupsList.EndedGroupsToggle.Click();
            List<String> expectedResult = new List<String> { "Lv-087-RD", "Lv-077-IOS" };
            wait.Until((d) => groupsList.AreGroupsVisible());
            List<String> actualResult = groupsList.GetAvailableGroupsNames(wait);
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
