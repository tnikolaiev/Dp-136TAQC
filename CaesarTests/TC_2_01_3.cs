using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_2_01_3 : BaseTest
    {              

        protected override void BeforeTest()
        {
            //Opening Schedule Page

            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Select group from LeftContainer

            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
        }

        [Test]

        public void IsMonthTabAvailable()
        {           

            //Assert MonthView tab is displayed
            Assert.IsTrue(MainPageInstance.CenterContainer
                .ScheduleContent
                .MonthTabInstance
                .IsMonthTabDisplayed(driver));          
        }

        [Test]

        public void IsWeekTabAvailable()
        {           

            //Click on WeekView button

            MainPageInstance.CenterContainer.ScheduleContent.WeekButton.Click();


            //Assert Week tab is displayed
            Assert.IsTrue(MainPageInstance
                .CenterContainer
                .ScheduleContent
                .WeekTabInstance
                .IsWeekTabDisplayed(driver));
        }

        [Test]

        public void IsKeyDatesTabAvailable()
        {

            //Click on KeyDates button

            MainPageInstance.CenterContainer.ScheduleContent.KeyDatesButton.Click();
            
            //Assert KeyDates tab is displayed
            Assert.IsTrue(MainPageInstance
                .CenterContainer
                .ScheduleContent
                .KeyDatesTabInstance
                .IsKeyDatesDisplayed(driver));
        }
      
    }

}

