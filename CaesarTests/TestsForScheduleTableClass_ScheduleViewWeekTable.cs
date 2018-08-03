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
    class TestsForScheduleTableClass_ScheduleViewWeekTable :BaseTest
    {      
        WeekTab weekTabInstance;       
        String textFromCell;
                

        protected override void BeforeTest()
        {

            //Opening Schedule Page
            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Select group from LeftContainer

            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
            
            //Go to Week tab

            MainPageInstance.CenterContainer.ScheduleContent.WeekButton.Click();

            // Create instance of Schedule Week table

            weekTabInstance  = new WeekTab(driver);             
          
        }

        [Test]

        public void GetCellTextBy_ValueInRowAndColumnName_ScheduleViewWeekTable()
        {
            textFromCell = weekTabInstance.ScheduleViewWeekTable.GetValueFromCell("9:30", "Tuesday");
           Assert.AreEqual(textFromCell, "MQC Lecture\r\nD. Petin\r\n305");
        }

        [Test]

        public void GetCellTextBy_RowNumberAndColumnNumber_ScheduleViewWeekTable()
        {
            textFromCell = weekTabInstance.ScheduleViewWeekTable.GetValueFromCell(1,2);      
            Assert.AreEqual(textFromCell, "MQC Practice\r\nD. Petin\r\n305");
        }

        [Test]

        public void GetCellTextBy_ValueInRowAndColumnNumber_ScheduleViewWeekTable()
        {
            textFromCell = weekTabInstance.ScheduleViewWeekTable.GetValueFromCell("9:00", 5);
            Assert.AreEqual(textFromCell, "Weekly Report\r\nD. Petin\r\n309");
        }

        [Test]

        public void GetCellTextBy_RowNumberAndColumnName_ScheduleViewWeekTable()
        {
            textFromCell = weekTabInstance.ScheduleViewWeekTable.GetValueFromCell(3,"Wednesday");
            Assert.AreEqual(textFromCell, "Expert Meeting\r\nN. Varenko\r\n309");

        }      

    }
}