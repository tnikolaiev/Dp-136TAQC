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
    class TC_2_01_3
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        ScheduleContent ScheduleContentInstance;        

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
            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.ScheduleItem);
        }

        [Test]

        public void IsMonthTabAvailable()
        {
            //Select group from LeftContainer
            ScheduleContentInstance = new ScheduleContent(driver);
            Acts.Click(ScheduleContentInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Assert MonthView tab is displayed
            Assert.IsTrue(ScheduleContentInstance
                .ScheduleMonthViewInstance
                .IsScheduleMonthViewDisplayed(driver));          
        }

        [Test]

        public void IsWeekTabAvailable()
        {
            //Select group from LeftContainer
            ScheduleContentInstance = new ScheduleContent(driver);
            Acts.Click(ScheduleContentInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Click on WeekView button

            Acts.Click(ScheduleContentInstance.WeekButton);


            //Assert Week tab is displayed
            Assert.IsTrue(ScheduleContentInstance
                .ScheduleWeekViewAndEditInstance
                .IsScheduleWeekViewDisplayed(driver));
        }

        [Test]

        public void IsKeyDatesTabAvailable()
        {
            //Select group from LeftContainer
            ScheduleContentInstance = new ScheduleContent(driver);
            Acts.Click(ScheduleContentInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Click on KeyDates button

            Acts.Click(ScheduleContentInstance.KeyDatesButton);


            //Assert KeyDates tab is displayed
            Assert.IsTrue(ScheduleContentInstance
                .ScheduleKeyDatesInstance
                .IsKeyDatesDisplayed(driver));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }

}

