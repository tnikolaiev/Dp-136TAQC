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
        MainPage mainPageInstance;

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
            ScheduleContentInstance = mainPageInstance.OpenScheduleContent();

            //Select group from LeftContainer

            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
        }

        [Test]

        public void IsMonthTabAvailable()
        {
           

            //Assert MonthView tab is displayed
            Assert.IsTrue(ScheduleContentInstance
                .MonthTabInstance
                .IsMonthTabDisplayed(driver));          
        }

        [Test]

        public void IsWeekTabAvailable()
        {
           

            //Click on WeekView button

            Acts.Click(ScheduleContentInstance.WeekButton);


            //Assert Week tab is displayed
            Assert.IsTrue(ScheduleContentInstance
                .WeekTabInstance
                .IsWeekTabDisplayed(driver));
        }

        [Test]

        public void IsKeyDatesTabAvailable()
        {            

            //Click on KeyDates button

            Acts.Click(ScheduleContentInstance.KeyDatesButton);


            //Assert KeyDates tab is displayed
            Assert.IsTrue(ScheduleContentInstance
                .KeyDatesTabInstance
                .IsKeyDatesDisplayed(driver));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }

}

