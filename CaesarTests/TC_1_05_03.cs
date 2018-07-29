using CaesarLib;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

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
        List<String> listOfCity;

        [OneTimeSetUp]
       // [SetUp]
        public void Initialize()
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            driver.Url = @"http://localhost:3000/logout";
            driver.Manage().Window.Maximize();
            loginPageInstance = new LoginPage(driver);
            wait.Until((d) => LoginPage.IsLoginPageOpened(d));
            loginPageInstance.LogIn("sasha", "1234");
            wait.Until((d) => MainPage.IsMainPageOpened(d));
            mainPageInstance = new MainPage(driver);
            TopMenu topMenuInstance = mainPageInstance.MoveToTopMenu();
            Acts.Click(topMenuInstance.LocationsItem);

        }
        //[Test]
        //public void ExecuteTest_ChooseLocation()
        //{
        //    locationWindowInstance = new LocationWindow(driver);
        //    string city =  "Lviv";
        //    IList<IWebElement> nonActiveCity = locationWindowInstance.GetLocationNonActiveWebElements();
            
          
        //     locationWindowInstance.ClickNonActiveButtonNames(nonActiveCity, city);
           
        //    Acts.Click(locationWindowInstance.ConfurmButton);
        //    groupLocationInstance = new CenterContainer(driver);
        //    Console.WriteLine(groupLocationInstance.GroupLocation.Text);
        //    string exeptualResultTitle = "Lviv";
        //    Assert.AreEqual(exeptualResultTitle, groupLocationInstance.GroupLocation.Text);
        //}

        [Test]

        public void ExecuteTest_ChooseListLocations()
        {

            locationWindowInstance = new LocationWindow(driver);
            List<string> listOfCity = new List <string> {"Lviv", "Dnipro"};
            IList<IWebElement> nonActiveCity = locationWindowInstance.GetLocationNonActiveWebElements();
            locationWindowInstance.ClickNonActiveButtonNames(nonActiveCity, listOfCity);
            Acts.Click(locationWindowInstance.ConfurmButton);
            CenterContainer groupLocationInstance = mainPageInstance.MoveToCenterContainer();
            groupLocationInstance = new CenterContainer(driver);
            wait.Until(groupLocationInstance.IsHintVisible());
            Console.WriteLine(groupLocationInstance.LocationHint.Text);
            string exeptualResultTitle = " Dnipro,Lviv";
            Assert.AreEqual(exeptualResultTitle, groupLocationInstance.LocationHint.Text);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}

