using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using CaesarLib;
using Motion;
using NLog;
using System.Xml.Linq;
using NUnit.Framework.Internal;

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

        static IEnumerable<object[]> GetLocationLists()
        {
           // return Instruments.ReadXML("LocationLists.xml", "testData", "city");
            var doc = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestData\LocationLists.xml);
        }

        [Test, TestCaseSource("LocationLists")]
        public void TestLocationList(string city)
        {
            locationWindowInstance = new LocationWindow(driver);
            List<string> listOfCity = new List<string>(city);
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

        private static IEnumerable<object[]> LocationLists()
        {
            //logger.Info("LocationLists");
            //var doc = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestData\LocationLists.xml);
            
            //return
            //    from vars in doc.Descendants("testData")
            //    let city = vars.Attribute("city").Value
            //    select new object[] { double.Parse(city) };



        }
    }
}