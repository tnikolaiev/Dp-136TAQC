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
    public class TC_2_02 : BaseTest
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
         wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
         MainPageInstance.TopMenu.ScheduleItem.Click();            

        }

        [Test]

        public void TwoGroupsSelectedSimultaneously()
        {
            //Selecting groups
            IWebElement FirstGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC");
            IWebElement SecondGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            FirstGroup.Click();
            SecondGroup.Click();
           
            //Check if groups are selected

            Assert.That((MainPageInstance.LeftContainer.GroupsInLocation.IsGroupChosen(FirstGroup) == true)&(MainPageInstance.LeftContainer.GroupsInLocation.IsGroupChosen(SecondGroup) == true));
          
        }

        [Test]

        public void IsWeekTableDisplayeForAllSelectedGroups()
        {
            //Selecting groups
            IWebElement FirstGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC");
            IWebElement SecondGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            FirstGroup.Click();
            SecondGroup.Click();

            //Navigating to Week Table
            ScheduleWeekTable table = MainPageInstance.CenterContainer.ScheduleContent.OpenWeekTab(wait).ScheduleViewWeekTable;

            //Check if activities count = 2
            IWebElement cell = table.GetCell("9:00", "Monday");
            Assert.AreEqual(2, table.ActivitiesCountInCell(cell));
        }

        [Test]

        public void IsKeyDatesTableDisplayeForAllSelectedGroups()
        {
            //Selecting groups
            IWebElement FirstGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC");
            IWebElement SecondGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            FirstGroup.Click();
            SecondGroup.Click();

            //Navigating to KeyDates Table
            Table table = MainPageInstance.CenterContainer.ScheduleContent.OpenKeyDatesTab(wait).KeyDatesTable;

            //Check if both groups exists in table

            Assert.That((table.GetValueFromCell(1, "Group") == "DP-093-JS") & (table.GetValueFromCell(2, "Group") == "DP-094-MQC"));
        }


        [Test]

        public void CheckingAllGroupsButtonSelectsAllGroups()
        {
            //Selecting all groups using AllGroups button
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();

            //Check if groups are selected
            IWebElement FirstGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC");
            IWebElement SecondGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            Assert.That((MainPageInstance.LeftContainer.GroupsInLocation.IsGroupChosen(FirstGroup) == true) & (MainPageInstance.LeftContainer.GroupsInLocation.IsGroupChosen(SecondGroup) == true));
        }

        [Test]

        public void CheckingAllGroupsButtonDeselectsAllGroups()
        {
            //Selecting and then deselecting all groups using AllGroups button
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();

            //Check if groups are deselected
            IWebElement FirstGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC");
            IWebElement SecondGroup = MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS");
            Assert.That((MainPageInstance.LeftContainer.GroupsInLocation.IsGroupChosen(FirstGroup) == false) & (MainPageInstance.LeftContainer.GroupsInLocation.IsGroupChosen(SecondGroup) == false));
        }

        [Test]

        public void ChekingAllGroupsButtonReturnsToDefaultState()
        {
            //Selecting all groups using AllGroups button
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();

            //Selecting one group
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Check AllGroups button is unchecked
            IWebElement AllGroupsButton = MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton;
            Assert.False(MainPageInstance.LeftContainer.GroupsInLocation.IsButtonChosen(AllGroupsButton));
            
        }

        [Test]

        public void ChekingAllGroupsButtonIsChecked()
        {
            //Selecting all groups using AllGroups button
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();            

            //Check AllGroups button is checked
            IWebElement AllGroupsButton = MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton;
            Assert.True(MainPageInstance.LeftContainer.GroupsInLocation.IsButtonChosen(AllGroupsButton));

        }

        [Test]

        public void ChekingAllGroupsButtonIsUnchecked()
        {
            //Selecting and then deselecting all groups using AllGroups button
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();
            MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton.Click();

            //Check AllGroups button is unchecked
            IWebElement AllGroupsButton = MainPageInstance.LeftContainer.GroupsInLocation.AllGroupsButton;
            Assert.False(MainPageInstance.LeftContainer.GroupsInLocation.IsButtonChosen(AllGroupsButton));

        }



    }
       
    }
