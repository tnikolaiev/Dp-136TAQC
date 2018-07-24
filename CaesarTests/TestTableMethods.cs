using CaesarLib;
using CaesarLib.MainPage;
using CaesarLib.StudentsPage;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarTests
{
    [TestFixture]
    class TestingTableMethods
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        ScheduleContent scheduleContentInstance;
        EditStudentList editStudentList;
        MainPageClass mainPageClassInstance;

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
            wait.Until((d) => MainPageClass.IsMainPageOpened(d));

            
        }

        [Test]

        public void TestCellSelectorOnKeyDatesTable()
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

            //Go to Key-Dates tab

            scheduleContentInstance.KeyDatesButton.Click();

            // Select some cell in key-dates table

            ScheduleKeyDatesTab scheduleKeyDatesTabInstance = new ScheduleKeyDatesTab(driver);
            string text = scheduleKeyDatesTabInstance.KeyDatesTable.GetCellBy2Keys("Demo2", "DP-094-MQC").Text;
            Console.WriteLine("Value from cell " + text);

        }

        [Test]

        public void TestCellSelectorOnStudentsList()
        {
            //Navigating to Students Page

            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.StudentsItem);


            //Select group from LeftContainer

            mainPageClassInstance = new MainPageClass(driver);

            Acts.Click(mainPageClassInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC"));

            //Go to table

            GroupView groupViewInstance = new GroupView(driver);
            groupViewInstance.EditButton.Click();

            //Select some cell using method CellSelector

            EditStudentList editStudentListInstance = new EditStudentList(driver);
            string text = editStudentListInstance
                .StudentTable
                .GetCellBy2Keys("English level", "Reaper Carolina")
                .Text;

            Console.WriteLine(text); 
        }

        [Test]

        public void TestNewCellSelectorOnStudentsList()
        {
            //Navigating to Students Page

            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.StudentsItem);


            //Select group from LeftContainer

            mainPageClassInstance = new MainPageClass(driver);

            Acts.Click(mainPageClassInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC"));

            //Go to table

            GroupView groupViewInstance = new GroupView(driver);
            groupViewInstance.EditButton.Click();

            //Select some cell using method CellSelector
            EditStudentList editStudentListInstance = new EditStudentList(driver);
            IWebElement tableElement = editStudentListInstance.StudentTable;
            Table table = new Table(tableElement, driver);
            String text1 = table.getValueFromCell(2,2);
            String text2 = table.getValueFromCell(2, "Name");

            Console.WriteLine(text1);
            Console.WriteLine(text2);
            
                


        }

    }

}
