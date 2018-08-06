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
        [SetUp]

        public void SetUp()
        {
            //Opening Caesar and Logging in
            driver.Url = baseURL;
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("qwerty", "1234", wait);

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            MainPageInstance.OpenScheduleContent(wait);
        }

        [Test]

        public void KeyDatesDisplayingOneGroup()
        {
        
            //Selecting group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Go to KeyDatesTab 
            MainPageInstance.CenterContainer.ScheduleContent.OpenKeyDatesTab(wait);
            
            string groupName = MainPageInstance.CenterContainer.ScheduleContent.KeyDatesTabInstance.KeyDatesTable.GetValueFromCell(1, "Group");
            Assert.AreEqual("DP-094-MQC", groupName);

        }
        [Test]

        public void KeyDatesDisplayingTwoGroups()
        {         
                     
            //Selecting groups
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();

            //Go to KeyDatesTab
            MainPageInstance.CenterContainer.ScheduleContent.OpenKeyDatesTab(wait);

            //Check groups which are displayed and count of rows in table
            string group1Name = MainPageInstance.CenterContainer.ScheduleContent.KeyDatesTabInstance.KeyDatesTable.GetValueFromCell(1, "Group"); 
            string group2Name = MainPageInstance.CenterContainer.ScheduleContent.KeyDatesTabInstance.KeyDatesTable.GetValueFromCell(2, "Group");
            int rowsCount = MainPageInstance.CenterContainer.ScheduleContent.KeyDatesTabInstance.KeyDatesTable.GetRows().Count;

            Assert.That("DP-093-JS" == group1Name & "DP-094-MQC" == group2Name & 2 == rowsCount);         
        }
    }
}