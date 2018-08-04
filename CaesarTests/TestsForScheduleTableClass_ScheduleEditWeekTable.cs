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
    class TestsForScheduleTableClass_ScheduleEditWeekTable : BaseTest
    {              
        EditScheduleWindow editscheduleWindowInstance;       
                

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

            //Select group from LeftContainer
            MainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Go to Schedule Editor
            MainPageInstance.CenterContainer.ScheduleContent.ScheduleCogwheell.Click();

            // Create instance of Schedule Edit Week Window
            editscheduleWindowInstance = new EditScheduleWindow(driver);

        }

        [Test]

        public void AddingEventToEditWeekTable()
        {          
            editscheduleWindowInstance.ScheduleEditWeekTable.GetValueFromCell("10:00", "Friday\r\n08/03");
            editscheduleWindowInstance.LectureEvent.Click();
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.TeacherControl, "D. Petin");
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.RoomControl, "740");
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.EventControl, "Special");
            editscheduleWindowInstance.ScheduleEditWeekTable.GetCell("10:00", "Friday\r\n08/03").Click();

        }

        [Test]

        public void CheckingActivityExists_EditWeekTable()
        {            
            editscheduleWindowInstance.LectureEvent.Click();
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.TeacherControl, "D. Petin");
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.RoomControl, "740");
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.EventControl, "Special");
            IWebElement cell = editscheduleWindowInstance.ScheduleEditWeekTable.GetCell("10:00", "Friday\r\n08/03");
            cell.Click();
            Assert.True(editscheduleWindowInstance.ScheduleEditWeekTable.IsActivityExists(cell));

        }

        [Test]

        public void CheckingActivityCorrect_EditWeekTable()
        {
            editscheduleWindowInstance.LectureEvent.Click();
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.TeacherControl, "D. Petin");
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.RoomControl, "740");
            Acts.SelectOptionFromDDL(editscheduleWindowInstance.EventControl, "Special");
            IWebElement cell = editscheduleWindowInstance.ScheduleEditWeekTable.GetCell("10:00", "Friday\r\n08/03");
            cell.Click();
            Assert.True(editscheduleWindowInstance.ScheduleEditWeekTable.IsActivityCorrect(cell, "Lecture\r\nD. Petin\r\n740"));
        }
       
    }
}