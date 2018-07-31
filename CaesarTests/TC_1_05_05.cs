﻿using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_05_05
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        CenterContainer groupLocationInstance;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;

        [OneTimeSetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("dmytro", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

        }
        [Test]
        public void ExecuteTest_ChooseLocationSofia_UsingDoubleClick_LocationPageOpened()
        {
            topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
            mainPageInstance.DoubleClick(mainPageInstance.ModalWindow.LocationWindow.CitySofia);
            string exeptualResultTitle = "Sofia";
            groupLocationInstance = new CenterContainer(driver);
            Console.WriteLine(groupLocationInstance.GroupLocation.Text);
            Assert.AreEqual(exeptualResultTitle, groupLocationInstance.GroupLocation.Text);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}

