﻿using CaesarLib;
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

            Acts.Click(scheduleContentInstance
                .LeftContainerInstance
                .GroupsInLocation
                .GetGroupByName("DP-093-JS"));


            //Go to Key-Dates tab

            scheduleContentInstance.KeyDatesButton.Click();

            // Select some cell in key-dates table

            KeyDatesTab scheduleKeyDatesTabInstance = new KeyDatesTab(driver);
            IWebElement tableElement = scheduleKeyDatesTabInstance.KeyDatesTable;  //.("Demo2", "DP-094-MQC").Text;
            Table table = new Table(tableElement, driver);
            string textFromCell = table.GetValueFromCell("DP-094-MQC", "Demo2");
            Console.WriteLine(textFromCell);

        }
    }
}