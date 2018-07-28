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
    class TestsForTableClass
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        ScheduleContent scheduleContentInstance;       
        Table table;

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

            //Select group from LeftContainer
            scheduleContentInstance = new ScheduleContent(driver);
            Acts.Click(scheduleContentInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            Acts.Click(scheduleContentInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-093-JS"));

            //Go to Key-Dates tab

            scheduleContentInstance.KeyDatesButton.Click();

            // Select some cell in key-dates table

            ScheduleKeyDatesTab scheduleKeyDatesTabInstance = new ScheduleKeyDatesTab(driver);
            IWebElement tableElement = scheduleKeyDatesTabInstance.KeyDatesTable;  //.("Demo2", "DP-094-MQC").Text;
            table = new Table(tableElement, driver);

        }

        [Test]

        public void GetCellTextByValueInRowAndColumnNameKeyDatesTable()
        {            
            string textFromCell = table.GetValueFromCell("DP-094-MQC", "Demo2");
            Assert.AreEqual(textFromCell, "03/14/2016");
        }

        [Test]

        public void GetCellTextByRowNumberAndColumnNumberKeyDatesTable()
        {
            string textFromCell = table.GetValueFromCell(2,5);
            Assert.AreEqual(textFromCell, "04/04/2016");
        }

        [Test]

        public void GetCellTextByValueInRowAndColumnNumberKeyDatesTable()
        {         
            string textFromCell = table.GetValueFromCell("DP-094-MQC", 3);
            Assert.AreEqual(textFromCell, "02/22/2016");
        }

        [Test]

        public void GetCellTextByRowNumberAndColumnNameKeyDatesTable()
        {
            string textFromCell = table.GetValueFromCell(1,"Finish");
            Assert.AreEqual(textFromCell, "11/28/2016");

        }
    }
}