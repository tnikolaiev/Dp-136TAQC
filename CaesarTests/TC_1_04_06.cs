using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_04_06
    {
        IWebDriver driver;
        Actions action;
        WebDriverWait wait;
        LoginPage loginPageInstance;
        MainPage mainPageInstance;

        [OneTimeSetUp]
        public void FirstInitialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [SetUp]
        public void Initialize()
        {            
            driver.Url = "http://localhost:3000/logout";
            action = new Actions(driver);            
            loginPageInstance = new LoginPage(driver);
            loginPageInstance.LogIn("dmytro", "1234", wait);
            mainPageInstance = new MainPage(driver);
        }

        static IEnumerable<object[]> StartFinishDateData = Instruments.ReadXML("StartFinishDateData.xml", "testData", "direction", "startDate", "finishDate");

        [Test, TestCaseSource("StartFinishDateData")]
        public void ExecuteTest_EnterStartDate_FinishDateFilled(String direction, String startDate, String finishDate)
        {
            var groupCreateWindow = mainPageInstance.ModalWindow.GroupCreateWindow;
            groupCreateWindow.Open(action, wait);
            groupCreateWindow.SetDirection(direction)
                .SetStartDate(startDate);
            groupCreateWindow.GroupNameField.Click();
            String actualResult = groupCreateWindow.FinishDateField.GetAttribute("value");
            
            Assert.AreEqual(finishDate, actualResult);
        }

        [TearDown]
        public void CleanUp()
        {
            Log4Caesar.Log();
        }

        [OneTimeTearDown]
        public void FinalCleanUp()
        {            
            driver.Quit();
        }
    }
}
