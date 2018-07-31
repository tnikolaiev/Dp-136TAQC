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
    class TestsForScheduleTableClass_ScheduleViewWeekTable
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        ScheduleContent scheduleContentInstance;
        WeekTab weekTabInstance;       
        String textFromCell;

        [SetUp]

        public void BeforeTest()
        {
            //Initializations and logging in Caesar
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
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
            
            //Go to Week tab

            scheduleContentInstance.WeekButton.Click();

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

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }

    }
}