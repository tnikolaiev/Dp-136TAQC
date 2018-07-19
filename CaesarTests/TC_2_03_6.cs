using CaesarLib;
using CaesarLib.Schedule;
using CaesarLib.StudentsPage;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_2_03_6
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
            driver.Url = baseURL;
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPage(d));

            //Opening Schedule Page
            TopMenu topMenuInstance = new TopMenu(driver);
            Actions builder = new Actions(driver);
            builder.MoveToElement(topMenuInstance.TopMenuSection).Build().Perform();
            Acts.Click(topMenuInstance.ScheduleItem);
        }

        [Test]

        public void CheckQAEventsScheduleEditor()
        {

            //Select group from LeftContainer
            schedulePageInstance = new SchedulePage(driver);
            Acts.Click(schedulePageInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-094-MQC"));

            //Click on cogweel for ScheduleEditor opening
            schedulePageInstance.ClickCogwheel();

            //Check if correct events present
            Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Lecture')]")));
            Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Weekly report')]")));
            Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Consultation')]")));
            Assert.IsTrue(Acts.IsElementPresent(driver, By.XPath("//li[@class='lectures-wrapper-button']/child::label[contains (text(),'Work with Expert')]")));


        }
        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }

}

