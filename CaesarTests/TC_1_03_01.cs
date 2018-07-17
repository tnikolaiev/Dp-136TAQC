using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_03_01
    {
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        IWebDriver driver = new ChromeDriver();
        WebDriverWait wait;

        [SetUp]
        public void Initialize()
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            driver.Url = @"http://localhost:3000/logout";
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("admin", "1234");
            mainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPage.IsMainPage(d));
        }

        [Test]
        public void ExecuteTest_FirstSignIn_AvailableGroupsList()
        {
            List<String> expectedResult = new List<String> { "Lv-084-QB", "Lv-045-DL", "Lv-023-UX" };
            wait.Until(mainPageInstance.LeftContainer.GroupsInLocation.AreGroupsVisible());
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_MyGroupsFilterCheckedUnchecked()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter);
            bool firstCondition = "myGroups pressed".Equals(Acts.GetAttribute(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter, "class"));
            mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter.Click();
            bool secondCondition = "myGroups".Equals(Acts.GetAttribute(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter, "class"));
            Assert.IsTrue(firstCondition && secondCondition);
        }

        [Test]
        public void ExecuteTest_MyGroupsFilterChecked_NoneAvailableGroups()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter);
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            Assert.IsEmpty(actualResult);
        }

        [Test]
        public void ExecuteTest_FutureGroupsFilter()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle);
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            Assert.IsEmpty(actualResult);
        }

        [Test]
        public void ExecuteTest_EndedGroupsFilter()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle);
            List<String> expectedResult = new List<String> { "Lv-087-RD", "Lv-077-IOS" };
            wait.Until(mainPageInstance.LeftContainer.GroupsInLocation.AreGroupsVisible());
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
