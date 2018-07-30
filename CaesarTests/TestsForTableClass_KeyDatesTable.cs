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
    class TestsForTableClass_KeyDatesTable
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        MainPage mainPageInstance;
        ScheduleContent scheduleContentInstance;
        KeyDatesTab scheduleKeyDatesTabInstance;       
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
            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-093-JS").Click();


            //Go to Key-Dates tab

            scheduleContentInstance.KeyDatesButton.Click();

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

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}