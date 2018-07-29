using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using CaesarLib;

namespace CaesarTests
{
 
    [TestFixture]
    public class TC_1_05_04
    {
        IWebDriver driver = new ChromeDriver();
        LoginPage loginPageInstance;
        WebDriverWait wait;
        LocationWindow locationWindowInstance;
        CenterContainer groupLocationInstance;
        TopMenu topMenuInstance;
        MainPage mainPageInstance;
        List<String> listOfCity;

        [SetUp]
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
        [TestCaseSource("LocationsList")]
        public void TestLocationList(string city)
        {
            locationWindowInstance = new LocationWindow(driver);
            List<string> listOfCity = new List<string> { "Lviv", "Dnipro" };
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
        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }
       
        private  IEnumerable<object[]> LocationsList()
        {
          //  logger.Info("LocationsList");
            var doc = XDocument.Load(@"LocationsList");
         //   logger.Info("\n" + doc.ToString());
            return
            from vars in doc.Descendants("vars")
            let city = vars.Attribute("city").Value
            select new object[] { city };
        }
    }
}