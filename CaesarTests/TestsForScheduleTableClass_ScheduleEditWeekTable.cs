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
    class TestsForScheduleTableClass_ScheduleEditWeekTable
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        ScheduleContent scheduleContentInstance;
        EditScheduleWindow editscheduleWindowInstance;       

        [SetUp]

        public void BeforeTest()
        {
            //Initializations and logging in Caesar
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));

            //Opening Schedule Page

           mainPageInstance = new MainPage(driver);
           scheduleContentInstance = mainPageInstance.OpenScheduleContent();

            //Select group from LeftContainer

            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Go toSchedule Editor

            scheduleContentInstance.ScheduleCogwheell.Click();

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

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }



    }
}