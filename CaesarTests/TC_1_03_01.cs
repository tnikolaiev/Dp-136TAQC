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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(4));
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
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_MyGroupsFilterCheckedUnchecked()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter);
            Assert.AreEqual("myGroups pressed", Acts.GetAttribute(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter, "class"));
            mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter.Click();
            Assert.AreEqual("myGroups", Acts.GetAttribute(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter, "class"));
        }

        [Test]
        public void ExecuteTest_MyGroupsFilterChecked_NoneAvailableGroups()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.MyGroupsFilter);
            List<String> expectedResult = new List<String> { };
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_FutureGroupsFilter()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.FutureGroupsToggle);
            List<String> expectedResult = new List<String> { };
            List<String> actualResult = mainPageInstance.LeftContainer.GroupsInLocation.GetAvailableGroupsNames();
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [Test]
        public void ExecuteTest_EndedGroupsFilter()
        {
            Acts.Click(mainPageInstance.LeftContainer.GroupsInLocation.EndedGroupsToggle);
            List<String> expectedResult = new List<String> { "Lv-087-RD", "Lv-077-IOS" };
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
