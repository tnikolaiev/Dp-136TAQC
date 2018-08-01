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
        ScheduleContent ScheduleContentInstance;
        MainPage mainPageInstance;

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

            mainPageInstance = new MainPage(driver);
           mainPageInstance.OpenScheduleContent();

            //Select group from LeftContainer

            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Click on cogweel for ScheduleEditor opening
            ScheduleContentInstance.ClickCogwheel();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(ScheduleContentInstance
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

            mainPageInstance = new MainPage(driver);
            mainPageInstance.OpenScheduleContent();

            //Select group from LeftContainer

            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Click on cogweel for ScheduleEditor opening
            ScheduleContentInstance.ClickCogwheel();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(ScheduleContentInstance
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

            mainPageInstance = new MainPage(driver);
           mainPageInstance.OpenScheduleContent();

            //Select group from LeftContainer

            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();

            //Click on cogweel for ScheduleEditor opening
            ScheduleContentInstance.ClickCogwheel();

            //Assert ScheduleEditor is displayed
            Assert.IsTrue(ScheduleContentInstance
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

