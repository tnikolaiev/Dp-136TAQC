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
    class TestsForScheduleTableClass
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        ScheduleContent scheduleContentInstance;
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


        }

        [Test]

        public void SelectCellInScheduleEditor()
        {
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

            //Go to ScheduleEditor
            scheduleContentInstance.ScheduleCogwheell.Click();
            EditScheduleWindow editScheduleWindowInstance = new EditScheduleWindow(driver);

            //Find table and using methods from ScheduleTable class
            IWebElement tableElement = editScheduleWindowInstance.ScheduleWeekTable;
            ScheduleWeekTable scheduleTable = new ScheduleWeekTable(tableElement, driver);
            scheduleTable.GetValueFromCell(4, 5);



        }
    }
}
