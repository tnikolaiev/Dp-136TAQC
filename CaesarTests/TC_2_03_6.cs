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
    class TC_2_03_6
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
             mainPageInstance.OpenScheduleContent();

            //Select group from LeftContainer

            mainPageInstance.LeftContainer.GroupsInLocation.GetGroupByName("DP-094-MQC").Click();
        }

        [Test]

        public void CheckQAEventsScheduleEditor()
        {           

            //Click on cogweel for ScheduleEditor opening
            ScheduleContentInstance.ClickCogwheel();

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

