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

    class TestsForTableClass_KeyDatesTable : BaseTest
    {             
        KeyDatesTab scheduleKeyDatesTabInstance;       
        String textFromCell;

       protected override void BeforeTest()
        {

            //Opening Schedule Page

            MainPageInstance = new MainPage(driver);
            wait.Until((d) => MainPageInstance.MoveToTopMenu().IsOpened());
            MainPageInstance.TopMenu.ScheduleItem.Click();

            //Select group from LeftContainer

            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
        MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();

            //Go to Key-Dates tab

            MainPageInstance.CenterContainer.ScheduleContent.KeyDatesButton.Click();

         // Select some cell in key-dates table

         scheduleKeyDatesTabInstance = new KeyDatesTab(driver);             
          
        }

        [Test]

        public void GetCellTextByValueInRowAndColumnNameKeyDatesTable()
        {
            textFromCell = scheduleKeyDatesTabInstance.KeyDatesTable.GetValueFromCell("DP-094-MQC", "Demo2");
            Assert.AreEqual(textFromCell, "03/14/2016");
        }

        [Test]

        public void GetCellTextByRowNumberAndColumnNumberKeyDatesTable()
        {
            textFromCell = scheduleKeyDatesTabInstance.KeyDatesTable.GetValueFromCell(2,5);
            Assert.AreEqual(textFromCell, "04/04/2016");
        }

        [Test]

        public void GetCellTextByValueInRowAndColumnNumberKeyDatesTable()
        {
            textFromCell = scheduleKeyDatesTabInstance.KeyDatesTable.GetValueFromCell("DP-094-MQC", 3);
            Assert.AreEqual(textFromCell, "02/22/2016");
        }

        [Test]

        public void GetCellTextByRowNumberAndColumnNameKeyDatesTable()
        {
            textFromCell = scheduleKeyDatesTabInstance.KeyDatesTable.GetValueFromCell(1,"Finish");
            Assert.AreEqual(textFromCell, "11/28/2016");

        }
       
    } 
}