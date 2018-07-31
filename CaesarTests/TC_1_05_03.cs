﻿using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace CaesarTests
{
    [TestFixture]
    class TC_1_05_03
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        LocationWindow locationWindowInstance;
        CenterContainer groupLocationInstance;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;

        //[OneTimeSetUp]
        [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("admin", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);

        }
        [Test]
        public void ExecuteTest_ChooseLocationRivne_UsingKeyBoard()
        {
            TopMenu topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
            locationWindowInstance = new LocationWindow(driver);
            Acts.Click(locationWindowInstance.CityRivne);
            Acts.PressKeyboardButton("{Enter}");
            string exeptualResultTitle = "Rivne";
            groupLocationInstance = new CenterContainer(driver);
            Console.WriteLine(groupLocationInstance.GroupLocation.Text);
            Assert.AreEqual(exeptualResultTitle, groupLocationInstance.GroupLocation.Text);
        }

        [Test]
        public void ExecuteTest_CancelLocationIvanoFrankivsk_UsingKeyBoard()
        {
            TopMenu topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);
            Acts.Click(mainPageInstance.ModalWindow.LocationWindow.CityIvanoFrankivsk);
            Acts.PressKeyboardButton("{Esc}");
            string exeptualResultTitle = "Lviv";
            groupLocationInstance = new CenterContainer(driver);
            Console.WriteLine(groupLocationInstance.GroupLocation.Text);
           // Console.WriteLine(groupLocationInstance.LocationHint.Text);
            Assert.AreEqual(exeptualResultTitle, groupLocationInstance.GroupLocation.Text);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Quit();
            driver.Close();
        }
    }
}

