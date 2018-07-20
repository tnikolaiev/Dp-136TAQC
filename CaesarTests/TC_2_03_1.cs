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
    class TC_2_03_1
    {
        IWebDriver driver;
        WebDriverWait wait;
        string baseURL = "localhost:3000";
        LoginPage loginPageInstance;
        SchedulePage schedulePageInstance;        

        [SetUp]
        public void BeforeTest()
        {
            //Initializations and logging in Caesar
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                        
        }

        [Test]

        public void IsScheduleEditorAvailableUnderAdmin()
        {
            //logging in under admin
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("qwerty", "1234");

            //Opening Schedule Page
            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.ScheduleItem);


            //Select group from LeftContainer
            schedulePageInstance = new SchedulePage(driver);
            Acts.Click(schedulePageInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Click on cogweel for ScheduleEditor opening
            schedulePageInstance.ClickCogwheel();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(schedulePageInstance
                .EditScheduleWindowInstance
                .IsScheduleEditorDisplayed(driver));          
        }

        [Test]

        public void IsScheduleEditorAvailableUnderCoordinator()
        {
            //logging in under admin
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("Dmytro", "1234");            

            //Opening Schedule Page
            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.ScheduleItem);


            //Select group from LeftContainer
            schedulePageInstance = new SchedulePage(driver);
            Acts.Click(schedulePageInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Click on cogweel for ScheduleEditor opening
            schedulePageInstance.ClickCogwheel();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(schedulePageInstance
                .EditScheduleWindowInstance
                .IsScheduleEditorDisplayed(driver));
        }

        [Test]

        public void IsScheduleEditorAvailableUnderTeacher()
        {
            //logging in under admin
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");

            //Opening Schedule Page
            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.ScheduleItem);


            //Select group from LeftContainer
            schedulePageInstance = new SchedulePage(driver);
            Acts.Click(schedulePageInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Click on cogweel for ScheduleEditor opening
            schedulePageInstance.ClickCogwheel();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(schedulePageInstance
                .EditScheduleWindowInstance
                .IsScheduleEditorDisplayed(driver));
        }


        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }

}

