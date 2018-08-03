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
    public class TC_2_01_2 : BaseTest
    {
        KeyDatesTab KeyDatesTabInstance;
        string groupName;

        [SetUp]

        public void SetUp()
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("qwerty", "1234", wait);

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();
        }

        [Test]

        public void KeyDatesDisplayingOneGroup()
        {
        
            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Go to KeyDatesTab and check groups which are displayed

            KeyDatesTabInstance = MainPageInstance.CenterContainer.ScheduleContent.OpenKeyDatesTab(wait);
            groupName = KeyDatesTabInstance.KeyDatesTable.GetValueFromCell(1, "Group");
            Assert.AreEqual("DP-094-MQC", groupName);

        }
        [Test]

        public void KeyDatesDisplayingTwoGroups()
        {         
                     
            //Selecting groups
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();

            //Go to KeyDatesTab and check data
            MainPageInstance.CenterContainer.ScheduleContent.KeyDatesButton.Click();
            wait.Until((d) => MainPageInstance.CenterContainer.ScheduleContent.KeyDatesTabInstance.IsKeyDatesDisplayed(driver));
            KeyDatesTabInstance = new KeyDatesTab(driver);
            groupName = KeyDatesTabInstance.KeyDatesTable.GetValueFromCell(1, "Group");
            Assert.AreEqual("DP-093-JS", groupName);
            groupName = KeyDatesTabInstance.KeyDatesTable.GetValueFromCell(2, "Group");
            Assert.AreEqual("DP-094-MQC", groupName);
            int rowsCount = KeyDatesTabInstance.KeyDatesTable.GetRows().Count;         
            Assert.AreEqual(2, rowsCount);

        }
    }
}